using ShopMVC.Models;
using ShopMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopMVC.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;

        public CartController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            var cart = (ShoppingCart)Session["cart"];

            return View(cart);
        }

        public ActionResult AddToCart(int id, int number)
        {
            var product = _context.Products.Single(p => p.Id == id);

            var cart = (ShoppingCart)Session["cart"];
            if (cart == null)
            {
                if(product.NumberInStock < number)
                    return RedirectToAction("Details", "Products", new { id = product.Id, error = "Liczba produktów nie może być większa, niż ich dostępna liczba." });
                cart = new ShoppingCart();
                var item = new Item { Product = product, Number = number };
                cart.Add(item);
                Session["cart"] = cart;
            }
            else
            {
                
                var index = cart.ShoppingCartIndex(id);
                if (index != -1)
                {
                    if(cart.UpdateNumber(index, number))
                        return RedirectToAction("Index");
                    else
                    {
                        
                        return RedirectToAction("Details", "Products", new { id = product.Id, error = "Liczba produktów nie może być większa, niż ich dostępna liczba." });
                    }
                }
                else
                {
                    var item = new Item { Product = product, Number = number };
                    cart.Add(item);
                }
                Session["cart"] = cart;

            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var cart = (ShoppingCart)Session["cart"];

            cart.Remove(id);

            return RedirectToAction("Index");
        }
    }    
}