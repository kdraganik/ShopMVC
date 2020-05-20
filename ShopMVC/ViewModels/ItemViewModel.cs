using ShopMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopMVC.ViewModels
{
    public class ItemViewModel
    {
        [Required]
        public Product Product { get; set; }

        [Required]
        [Display(Name = "Ilość")]
        public int Number { get; set; }

        public string ErrorMessage { get; set; }
    }
}