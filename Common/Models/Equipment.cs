using System.Collections.Generic;

namespace Common.Models
{
    public class Equipment : BaseEntity
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
    }
}
