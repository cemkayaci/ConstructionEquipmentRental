using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class Types : BaseEntity
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
    }
}
