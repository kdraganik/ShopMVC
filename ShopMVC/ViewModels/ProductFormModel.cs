using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMVC.ViewModels
{
    public class ProductFormModel
    {
        public Product Product { get; set; }

        public string Header { get; set; }
    }
}