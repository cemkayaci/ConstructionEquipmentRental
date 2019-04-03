using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messages.Invoice
{
    public class GetInvoice
    {
        public int RentId { get; set; }
        public string UserName { get; set; }
    }
}
