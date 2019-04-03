using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messages.Rent
{
    public class RentCreated
    {
        public int InventoryId { get; set; }
        public string EquipmentName { get; set; }
        public int LoyalityPoint { get; set; }
        public int Days { get; set; }         
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }        
    }
}
