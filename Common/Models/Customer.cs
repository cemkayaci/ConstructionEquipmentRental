using System.Collections.Generic;

namespace Common.Models
{
    public class Customer : BaseEntity
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }        

        public ICollection<Rent> Rents { get; set; }
    }
}
