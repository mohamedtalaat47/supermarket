using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace supermarket.Models
{
    public class PointOfSale
    {
        [Key]
        public int Id { get; set; }

        public decimal Balance { get; set; }

        [ForeignKey(nameof(Shift))]
        public int ShiftId { get; set; }

        public Shift Shift { get; set; }

        public IList<Transaction> Transactions { get; set; }
    }
}

