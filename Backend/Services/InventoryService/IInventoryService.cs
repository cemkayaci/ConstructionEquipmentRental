using System.Threading.Tasks;
using Common.Models;
using System.Collections.Generic;

namespace Backend.Services.InventoryService
{
    public interface IInvetoryService
    {
        Task<List<Inventory>> GetInventories();
    }
}
