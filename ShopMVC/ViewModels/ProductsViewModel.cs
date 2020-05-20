using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMVC.ViewModels
{
    public class ProductsViewModel
    {
        public List<Product> ListOfProducts { get; set; }

        public string Query { get; set; }

        public string Sort { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int Max { get; set; }
    }
}