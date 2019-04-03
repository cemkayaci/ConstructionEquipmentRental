using Common.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services.InventoryService
{
    public class InvoiceService : IInvoiceService
    {
        private readonly DataContext _dataContext;

        public InvoiceService(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<List<Rent>> GetInvoices(string customerName)
        {
            var query = _dataContext.Rents.Include(rentDetails => rentDetails.RentDetails).AsQueryable();

            query = query.Where(rent => rent.Customer.CustomerName == customerName);

            return await query.ToListAsync();            
             
        }
        
        public async Task<Rent> GetInvoice(string customerName, int rentId)
        {
            var query = _dataContext.Rents.Include(rent =>rent.Customer).Include(rentDetails => rentDetails.RentDetails)
                                                .ThenInclude(rentDetails => (rentDetails as RentDetails).Inventory)
                                                .ThenInclude(inventory => inventory.Equipment)
                                          .Include(rentDetails => rentDetails.RentDetails)
                                                 .ThenInclude(rentDetails => (rentDetails as RentDetails).Inventory)
                                                 .ThenInclude(inventory => inventory.Types).AsQueryable();

            query = query.Where(rent => rent.Customer.CustomerName == customerName && rent.RentId == rentId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
