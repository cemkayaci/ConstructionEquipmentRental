using Backend.Services.InventoryService;
using Common.Enum;
using Common.Messages;
using Common.Messages.Invoice;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Backend.Chain
{
    public class InvoiceHandler : AbstractHandler
    {
        public IInvoiceService InvoiceService { get; set; }

        public override async Task<object> Handle(Message message)
        {
            if (RequestType.GetInvoice == message.RequestType)
            {
                var messageContent = JsonConvert.DeserializeObject<GetInvoice>(message.Content);
                return JsonConvert.SerializeObject(await InvoiceService.GetInvoice(messageContent.UserName, messageContent.RentId));
            }
            else if(RequestType.GetInvoiceList == message.RequestType)
            {
                var messageContent = JsonConvert.DeserializeObject<GetInvoice>(message.Content);
                return JsonConvert.SerializeObject(await InvoiceService.GetInvoices(messageContent.UserName));
            }
            else
            {
                return await base.Handle(message);
            }
        }
    }
}
