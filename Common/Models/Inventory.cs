using System.Collections.Generic;

namespace Common.Models
{
    public class Inventory : BaseEntity
    {
        public int InventoryId { get; set; }

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        public int TypeId { get; set; }
        public Types Types { get; set; }
       
        public List<RentDetails> RentDetails { get; set; }
    }
}
