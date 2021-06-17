using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
