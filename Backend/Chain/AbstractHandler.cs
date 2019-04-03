using Common.Messages;
using System.Threading.Tasks;

namespace Backend.Chain
{
    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
          
            return handler;
        }

        public virtual async Task<object> Handle(Message message)
        {
            if (this._nextHandler != null)
            {
                return await this._nextHandler.Handle(message);               
            }
            else
            {
                return null;
            }
        }
    }
}
