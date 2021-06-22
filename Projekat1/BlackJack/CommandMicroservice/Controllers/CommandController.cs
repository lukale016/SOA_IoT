using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CommandMicroservice.Hubs;
using CommandMicroservice.Models;
using CommandMicroservice.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommandMicroservice.Controllers
{
    [EnableCors("SOAPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommandController : ControllerBase
    {
        public ISensorRepository _sensorRepository;

        private DataHub _hub;

        public CommandController(ISensorRepository sensorRepository, DataHub hub)
        {
            _sensorRepository = sensorRepository;
            _hub = hub;
        }
        
        [HttpGet("{sensorType}")]
        public async Task<IEnumerable<SensorCommand>> GetData([FromRoute] string sensorType)
        {
            var sensors =  await _sensorRepository.GetData(sensorType);
            return sensors;
        }

        [HttpPost]
        public async Task PostCommand([Required, FromBody] string command)
        {
            HttpClient httpClient = new HttpClient();
            if (command == "card3")
            {
                await _hub.SendWarning(command, "Warning: Possible bust");
                var responseMessage = await httpClient.PostAsJsonAsync("http://HandThree/api/Data/PostStop", command);
            }
            else if (command == "card1" || command == "card2")
            {
                if(command == "card1")
                    await _hub.SendWarning(command, "Warning: Mediocre start.");
                if(command == "card2")
                    await _hub.SendWarning(command, "If card1 was not mediocre you have, a great hand.");
                var responseMessage = await httpClient.PostAsJsonAsync("http://HandOneAndTwo/api/Data/PostStop", command);
            }
        }
    }
}
