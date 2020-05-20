using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopMVC.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa produkutu")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Cena")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Link do zdjęcia")]
        public string Photo { get; set; }

        [Required]
        [Display(Name = "Dostępne")]
        public int NumberInStock { get; set; }

    }
}