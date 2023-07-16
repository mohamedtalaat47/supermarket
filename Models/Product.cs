using System;
using System.ComponentModel.DataAnnotations;

namespace supermarket.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Stock is required.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }
    }

}

