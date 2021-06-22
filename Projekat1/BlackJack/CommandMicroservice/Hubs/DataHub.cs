using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace CommandMicroservice.Hubs
{
    public class DataHub : Hub
    {
        public async Task SendWarning(string type, string message)
        {
            await Clients.All.SendAsync("receivedData", type, message);
        }
    }
}