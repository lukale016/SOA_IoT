using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace DataMicroservice.Hubs
{
    public class DataHub : Hub
    {
        public async Task SendData(string type, int value, string timestamp)
        {
            await Clients.All.SendAsync("sendData", type, value, timestamp);
        }
    }
}