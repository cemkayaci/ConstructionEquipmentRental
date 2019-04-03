using Common.Messages;
using System.Threading.Tasks;

namespace Backend.Chain
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        Task<object> Handle(Message message);
    }
}
