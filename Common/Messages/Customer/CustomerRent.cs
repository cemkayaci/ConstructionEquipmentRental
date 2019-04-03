using Common.Messages.Rent;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messages.Customer
{
    public class CustomerRent
    {
        public string CustomerName { get; set; }
        public List<RentCreated> RentCreated { get; set; }
    }
}
