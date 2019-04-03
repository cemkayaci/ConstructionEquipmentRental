using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Backend.Services.InventoryService
{
    public class InvetoryService : IInvetoryService
    {
        private readonly DataContext _dataContex;

        public InvetoryService(DataContext dataContex)
        {
            this._dataContex = dataContex;
        }
        public async Task<List<Inventory>> GetInventories()
        {
           return await _dataContex.Inventories.Include(inc => inc.Equipment).Include(inc => inc.Types).ToListAsync();                     
        }
    }
}
