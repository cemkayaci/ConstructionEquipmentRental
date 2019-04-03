using Common.Messages.Rent;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services.RentService
{
    public interface IRentService
    {
        Task<bool> CreateRent(string UserName, List<RentCreated> RentCreated);        
    }
}
