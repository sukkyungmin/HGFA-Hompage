using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HangilFA.Hubs
{
    public class HgfaHub : Hub<IHgfaClient>
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }
        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.ReceiveMessage(message);
        }

    }

    public interface IHgfaClient
    {
        Task ReceiveMessage(string user, string message);
        Task ReceiveMessage(string messaige);
    }
   
}
