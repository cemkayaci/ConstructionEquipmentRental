using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Enum;
using Common.Messages;
using Common.Messages.Inventory;
using Common.Models;
using Frontend.Context;
using Frontend.Models; 
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Frontend.Services;
using System.Text;
using Frontend.Helpers.Alert;
using Frontend.Helpers.Exception;

namespace Frontend.Controllers
{
    [ServiceFilter(typeof(ExceptionHandler))]
    public class RentController : Controller
    {
        private readonly INServiceBusRequester<Message> _nServiceBusRequester;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public RentController(INServiceBusRequester<Message> nServiceBusRequester, IMapper mapper, DataContext dataContext)
        {
            this._nServiceBusRequester = nServiceBusRequester;
            this._mapper = mapper;
            this._dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            var message = new Message();
            message.Id = Guid.NewGuid();
            message.RequestType = RequestType.InventoryList;
            message.Content = JsonConvert.SerializeObject(new InventoryRequest());
            var response = await _nServiceBusRequester.GetMessage(message);
            var inventories = JsonConvert.DeserializeObject<List<Inventory>>(response.Content);

            var listview = _mapper.Map<List<RentViewModel>>(inventories);

            return View(listview);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Index(List<RentViewModel> rentViewModels)
        {

            if (ModelState.IsValid)
            {
                var selectedModels = rentViewModels.Where(x => x.IsChecked == true).ToList();
                var cartDetails = _mapper.Map<List<Cart>>(selectedModels);

                await _dataContext.Carts.AddRangeAsync(cartDetails);

                int save = await _dataContext.SaveChangesAsync();
                if (save > 0)
                {
                    return RedirectToAction(nameof(Index)).AlertSuccess("Success", "Your process has been completed successfully.");
                }

            }

            var errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);

            var builder = new StringBuilder();

            foreach (var error in errors)
            {
                builder.Append(error);
            }

            return RedirectToAction(nameof(Index)).AlertDanger("Error", builder.ToString());
             
        }
    }
}
