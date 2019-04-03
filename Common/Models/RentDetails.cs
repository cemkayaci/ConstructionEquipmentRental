using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class RentDetails : BaseEntity
    {
        public int RentDetailId { get; set; }

        public int RentId { get; set; }
        public Rent Rent { get; set; }

        public int InventoryId { get; set; }
        
        public Inventory Inventory { get; set; }

        public int LoyalityPoint { get; set; }
        public int Days { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price  { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
