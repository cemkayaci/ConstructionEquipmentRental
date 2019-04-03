using Backend.Services.InventoryService;
using Common.Enum;
using Common.Messages;
using Common.Messages.Inventory;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace Backend.Chain
{
    public class InventoryHandler : AbstractHandler
    {         
        public IInvetoryService inventoryService  { get; set; }

        public override async Task<object> Handle(Message message)
        {
            if (RequestType.InventoryList == message.RequestType)
            {                
                var messageContent =  JsonConvert.DeserializeObject<InventoryRequest>(message.Content);                 
                return JsonConvert.SerializeObject(await inventoryService.GetInventories());
            }
            else
            {
              return  await base.Handle(message);
            }
        }
    }
}
