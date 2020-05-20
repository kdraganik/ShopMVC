using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopMVC.Models
{
    public class Item
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public virtual Product Product { get; set; }

        [Required]
        [Display(Name = "Ilość")]
        public int Number { get; set; }
    }
}