using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class Rent : BaseEntity
    {
        public int RentId { get; set; }

        public List<RentDetails> RentDetails { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public DateTime DateAdded { get; set; }

        public Rent()
        {
            RentDetails = new List<RentDetails>();
        }

    }
}
