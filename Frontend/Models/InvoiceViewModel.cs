using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public class InvoiceViewModel
    {
        public int RentId { get; set; }
        public int TotalLoyalityPoints { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateAdded { get; set; }
        
    }
}
