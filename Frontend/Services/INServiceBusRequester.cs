using Common.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services
{
    public interface INServiceBusRequester<T> where T : Message
    {
        Task<T> GetMessage(T message);
    }
}
