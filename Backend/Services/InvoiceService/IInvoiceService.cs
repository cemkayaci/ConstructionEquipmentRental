using Backend.Helper;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services.InventoryService
{
    public interface IInvoiceService
    {
        Task<List<Rent>> GetInvoices(string customerName);
        Task<Rent> GetInvoice(string customerName, int rentId);
    }
}
