using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace supermarket.Models
{
	public class Transaction
	{
        [Key]
		public int Id { get; set; }

        [ForeignKey(nameof(PointOfSale))]
		public int PointOfSaleId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [RegularExpression("^(in|out)$", ErrorMessage = "Type must be 'in' or 'out'.")]
        public string Type { get; set; }

		public PointOfSale PointOfSale { get; set; }

        public IList<Order> Orders { get; set; }

        public IList<Refund> Refunds { get; set; }
    }
}

