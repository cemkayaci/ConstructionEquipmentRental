using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Enum;
using Common.Messages;
using Common.Messages.Customer;
using Common.Messages.Rent;
using Frontend.Context;
using Frontend.Helpers.Alert;
using Frontend.Helpers.Exception;
using Frontend.Helpers.Rental;
using Frontend.Models;
using Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Frontend.Controllers
{
    [ServiceFilter(typeof(ExceptionHandler))]
    public class CartController : Controller
    {
        private readonly INServiceBusRequester<Message> _nServiceBusRequester;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CartController(INServiceBusRequester<Message> nServiceBusRequester, DataContext dataContext, IMapper mapper)
        {
            this._nServiceBusRequester = nServiceBusRequester;
            this._dataContext = dataContext;
            this._mapper = mapper;
        }
         
        public async Task<ActionResult> Index()
        {
            var carts = await _dataContext.Carts.Where(x=>x.UserName== UserDetails.UserName).ToListAsync();
            return View(carts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(List<Cart> carts)
        {
            if (ModelState.IsValid)
            {
                var rentCreates = _mapper.Map<List<RentCreated>>(carts);

                CustomerRent customerRent = new CustomerRent();
                customerRent.CustomerName = UserDetails.UserName;
                customerRent.RentCreated = rentCreates;

                var message = new Message();
                message.Id = Guid.NewGuid();
                message.RequestType = RequestType.RentCreate;
                message.Content = JsonConvert.SerializeObject(customerRent);

                var response = await _nServiceBusRequester.GetMessage(message);
                var created = JsonConvert.DeserializeObject<bool>(response.Content);                 
                if(created)
                {
                    _dataContext.RemoveRange(await _dataContext.Carts.ToListAsync());
                     await _dataContext.SaveChangesAsync();
                    
                     return RedirectToAction(nameof(Index)).AlertSuccess("Success", "Your process has been completed successfully.");                
                }
                 
            }

            return View().AlertDanger("Error", "Your transaction was not completed successfully.");
        }       
        

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {        
             var order = await _dataContext.Carts.FirstOrDefaultAsync(t => t.Id == id);               
             return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var order = await _dataContext.Carts.FirstOrDefaultAsync(t => t.Id == id);
            if (order != null)
            {
                _dataContext.Carts.Remove(order);
                int save =  await _dataContext.SaveChangesAsync();
                if (save > 0)
                {
                    return RedirectToAction(nameof(Index)).AlertSuccess("Success", "Your process has been completed successfully.");
                }
            }

            return View().AlertDanger("Error", "Your transaction was not completed successfully.");
        }

    }
}