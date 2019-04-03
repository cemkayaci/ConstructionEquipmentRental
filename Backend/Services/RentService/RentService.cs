using AutoMapper;
using Common.Messages.Rent;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Backend.Services.RentService
{
    public class RentService : IRentService
    {         
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public RentService(DataContext dataContext, IMapper mapper)
        {        
            this._dataContext = dataContext;
            this._mapper = mapper;       
        }

        public async Task<bool> CreateRent(string UserName, List<RentCreated> RentCreated)
        {
            var rentDetails = _mapper.Map<List<RentDetails>>(RentCreated);

            var customer = await _dataContext.Customers.FirstOrDefaultAsync(x => x.CustomerName == UserName);

            if (customer ==null)
            {
               await _dataContext.Customers.AddAsync(new Customer() { CustomerName = UserName});
               await _dataContext.SaveChangesAsync();
            }

            customer = await _dataContext.Customers.FirstOrDefaultAsync(x => x.CustomerName == UserName);

            Rent rent = new Rent();
            rent.DateAdded = DateTime.Now;
            rent.CustomerId = customer.CustomerId;
            rent.RentDetails = rentDetails;

            await _dataContext.Rents.AddAsync(rent);
            bool save = await _dataContext.SaveChangesAsync() > 0;            

            return save;
        }
    }
}
