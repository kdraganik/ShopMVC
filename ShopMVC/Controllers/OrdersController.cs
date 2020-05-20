using Microsoft.AspNet.Identity;
using ShopMVC.Models;
using ShopMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace ShopMVC.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;

        public OrdersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var orders = _context.Orders.ToList();
            var viewModel = new OrdersViewModel { Orders = orders };
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var order = _context.Orders.Include(o => o.ItemsList.Select(l => l.Product)).Include(o => o.User).Single(o => o.Id == id);
            if (order == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();
            if(userId == null)
                return RedirectToAction("Login", "Account");
            var user = _context.Users.Single(u => u.Id == userId);

            if (order.User == user || User.IsInRole("Admin"))
                return View(order);

            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public ActionResult NewOrder()
        {
            var cart = (ShoppingCart)Session["cart"];
            var items = cart.Items;
            var userId = User.Identity.GetUserId();
            var user = _context.Users.Single(u => u.Id == userId);
            if (user == null)
                return HttpNotFound();
            var order = 
                new Order
                {
                    User = user,
                    ItemsList =
                        items.Select(i =>
                            new Item()
                            {
                                Number = i.Number,
                                Product = _context.Products.FirstOrDefault(p => p.Id == i.Product.Id)
                            }).ToList()
                };
            _context.Orders.Add(order);

            foreach (var item in items)
            {
                var productFromDb = _context.Products.Single(p => p.Id == item.Product.Id);
                productFromDb.NumberInStock = item.Product.NumberInStock - item.Number;
            }
            _context.SaveChanges();

            Session["cart"] = null;
            return View("Details", order);
        }
    }
}