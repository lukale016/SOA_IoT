using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CommandMicroservice.Models;
using CommandMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommandMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommandController : ControllerBase
    {
        public ISensorRepository _sensorRepository;
        public CommandController(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
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
                var responseMessage = await httpClient.PostAsJsonAsync("http://HandThree/api/Data/PostStop", command);
            }
            else if (command == "card1" || command == "card2")
            {
                var responseMessage = await httpClient.PostAsJsonAsync("http://HandOneAndTwo/api/Data/PostStop", command);
            }
        }
    }
}
