using ShopMVC.Models;
using ShopMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopMVC.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext _context;

        public ProductsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index(string query = null, string sort = "name", int page = 1, int pageSize = 6)
        {
            IQueryable<Product> productsQuery = _context.Products;

            if (!String.IsNullOrWhiteSpace(query))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(query));

            }

            if (sort == "name")
                productsQuery = productsQuery.OrderBy(p => p.Name);
            else if(sort == "price")
                productsQuery = productsQuery.OrderBy(p => p.Price);

            productsQuery = productsQuery.Skip((page - 1) * pageSize).Take(pageSize);
            
            var products = productsQuery.ToList();
            double max = (double) _context.Products.Count() / pageSize;
            var viewModel = new ProductsViewModel
            {
                ListOfProducts = products,
                Query = query,
                Sort = sort,
                Page = page,
                PageSize = pageSize,
                Max = (int) Math.Ceiling(max)
            };

            
            if (User.IsInRole("Admin"))
            {
                return View("IndexAdmin", viewModel);
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Details(int id, string error = null)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
                return HttpNotFound();

            var cart = (ShoppingCart) Session["cart"];

            if (cart != null)
            {
                var index = cart.ShoppingCartIndex(id);

                if (index != -1)
                    product.NumberInStock = product.NumberInStock - cart.Items[index].Number;
            }

            var item = new ItemViewModel
            {
                Product = product,
                Number = 1,
                ErrorMessage = error
            };

            if (User.IsInRole("Admin"))
            {
                return View("DetailsAdmin", item);
            }

            return View(item);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            var viewModel = new ProductFormModel
            {
                Product = new Product(),
                Header = "Dodaj nowy produkt"
            };

            return View("ProductForm", viewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
                return HttpNotFound();

            var viewModel = new ProductFormModel
            {
                Product = product,
                Header = "Edytuj produkt"
            };

            return View("ProductForm", viewModel);
        }

        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product product)
        {
            if (!ModelState.IsValid)
                return View("ProductForm", product);

            if(product.Id == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                var productFromDb = _context.Products.Single(p => p.Id == product.Id);
                productFromDb.Name = product.Name;
                productFromDb.Price = product.Price;
                productFromDb.Description = product.Description;
                productFromDb.Photo = product.Photo;
                productFromDb.NumberInStock = product.NumberInStock;

            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Products");
        }

        public ActionResult Delete(Product product)
        {
            var productFromDb = _context.Products.Single(p => p.Id == product.Id);

            _context.Products.Remove(productFromDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Products");
        }
    }
}