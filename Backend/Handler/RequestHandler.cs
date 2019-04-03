using Common.Messages;
using NServiceBus;
using System; 
using System.Threading.Tasks; 
using Backend.Chain;
using NServiceBus.Logging;

namespace Backend.Handler
{
    public class RequestHandler : IHandleMessages<Message>
    {
        public RentHandler rentHandler { get; set; }
        public InventoryHandler inventoryHandler { get; set; }
        public InvoiceHandler invoiceHandler { get; set; }

        static ILog log = LogManager.GetLogger<RequestHandler>();

        public async Task Handle(Message message, IMessageHandlerContext context)
        {
            var response = new Message();
            response.Id = message.Id;
            response.RequestType = message.RequestType;

            try
            {
                rentHandler.SetNext(inventoryHandler).SetNext(invoiceHandler);

                log.Info(string.Format("Message handled Details : {0}  Content {1}", message.Id, message.Content));

                var result = await rentHandler.Handle(message);                      

                response.Content = Convert.ToString(result);
                response.IsSucceded = true;
                log.Info(string.Format("Message sended back Details : {0}  Content {1}", message.Id, result));

            }
            catch(Exception ex)
            {
                log.Error("Message handling failed. Details :", ex.InnerException);
            }

            await context.Reply(response).ConfigureAwait(false);
        }         
    }
}
