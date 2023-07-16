using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace supermarket.Models
{
    public class Refund
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Transaction))]
        public int TransactionId { get; set; }
        

        public decimal TotalPrice { get; set; }

        public Transaction Transaction { get; set; }
        public IList<RefundItem> RefundItems { get; set; }
    }
}

