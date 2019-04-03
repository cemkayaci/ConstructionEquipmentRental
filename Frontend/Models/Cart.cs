using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Frontend.Models
{
    public class Cart
    {  
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public string EquipmentName { get; set; }
        public int LoyalityPoint { get; set; }
        public int Days { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserName { get; set; }

        public Cart()
        {
            DateAdded = DateTime.Now;
        }
    }
}
