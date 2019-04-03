using Common.Messages;
using NServiceBus;
using System.Threading.Tasks;

namespace Frontend.Services
{
    public class NServiceBusRequester<T> : INServiceBusRequester<T> where T : Message
    {
        private readonly IEndpointInstance _endpointInstance;

        public NServiceBusRequester(IEndpointInstance endpointInstance)
        {
            this._endpointInstance = endpointInstance;           
        }

        public async Task<T> GetMessage(T message)
        {
            var sendOptions = new SendOptions();
            sendOptions.SetDestination("Backend.Endpoint");
            var responseTask = await _endpointInstance.Request<T>(message, sendOptions);
            return responseTask;
        }

    }
}
