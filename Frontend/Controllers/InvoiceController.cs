using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Enum;
using Common.Messages;
using Common.Messages.Invoice;
using Common.Models;
using Frontend.Helpers.Alert;
using Frontend.Helpers.Exception;
using Frontend.Helpers.Rental;
using Frontend.Models;
using Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Frontend.Controllers
{
    [ServiceFilter(typeof(ExceptionHandler))]
    public class InvoiceController : Controller
    {
        private readonly INServiceBusRequester<Message> _nServiceBusRequester;
        private readonly IMapper _mapper;

        public InvoiceController(INServiceBusRequester<Message> nServiceBusRequester, IMapper mapper)
        {
            this._nServiceBusRequester = nServiceBusRequester;
            this._mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var message = new Message();
            message.Id = Guid.NewGuid();
            message.RequestType = RequestType.GetInvoiceList;
            message.Content = JsonConvert.SerializeObject(new GetInvoice() { UserName = UserDetails.UserName });
            var response = await _nServiceBusRequester.GetMessage(message);
            if(response.IsSucceded)
            {
                var invoices = JsonConvert.DeserializeObject<List<Rent>>(response.Content);
                 
                var invoiceRecords = _mapper.Map<List<InvoiceViewModel>>(invoices);
                return View(invoiceRecords);
            }

            return View().AlertDanger("Error", "Your request couldn't processed.");

        }

        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            if (ModelState.IsValid)
            {
                var message = new Message();
                message.Id = Guid.NewGuid();
                message.RequestType = RequestType.GetInvoice;
                message.Content = JsonConvert.SerializeObject(new GetInvoice() { UserName = UserDetails.UserName, RentId = id });
                var response = await _nServiceBusRequester.GetMessage(message);
                if (response.IsSucceded)
                {
                    var invoice = JsonConvert.DeserializeObject<Rent>(response.Content);
                    string invoiceRaw =   invoice.GenerateInvoce();
                    var content = Encoding.UTF8.GetBytes(invoiceRaw);

                    return File(content, "text/plain", "Invoice.txt");
                }

                return View().AlertDanger("Error", "Your request couldn't processed.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}