using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace supermarket.Models
{
    public class RefundItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Refund))]
        public int RefundId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        public Refund Refund { get; set; }

        public Product Product { get; set; }
    }

}

