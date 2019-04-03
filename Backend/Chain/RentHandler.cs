using Backend.Services.RentService;
using Common.Enum;
using Common.Messages;
using Common.Messages.Customer;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Backend.Chain
{
    public class RentHandler : AbstractHandler
    {      
        public IRentService rentService;

        public override async Task<object> Handle(Message message)
        {
            if (RequestType.RentCreate == message.RequestType)
            {
                var messageContent = JsonConvert.DeserializeObject<CustomerRent>(message.Content);
                bool result = await rentService.CreateRent(messageContent.CustomerName, messageContent.RentCreated);   
                return JsonConvert.SerializeObject(message.IsCreated = result);
            }            
            else
            {
                return await base.Handle(message);
            }
        }
    }
}
