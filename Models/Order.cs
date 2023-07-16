using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace supermarket.Models
{
	public class Order
	{
        [Key]
		public int Id { get; set; }
        public decimal DiscountAmount { get; set; } = 0;

        public decimal DeliveryFees { get; set; } = 0;

        public decimal TotalPrice { get; set; }

        [ForeignKey(nameof(Transaction))]
        public int TransactionId { get; set; }

        public Transaction Transaction { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}

