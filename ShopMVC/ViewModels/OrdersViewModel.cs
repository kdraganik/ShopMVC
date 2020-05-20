using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMVC.ViewModels
{
    public class OrdersViewModel
    {
        public List<Order> Orders { get; set; }
    }
}