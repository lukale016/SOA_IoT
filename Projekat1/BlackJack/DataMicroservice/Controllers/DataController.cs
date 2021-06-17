using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataMicroservice.Models;
using DataMicroservice.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataMicroservice.Controllers
{
    [EnableCors("SOAPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DataController : ControllerBase
    {
        public ISensorRepository _dataRepository;
        public DataController(ISensorRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Sensor sensor)
        {
            Console.WriteLine("Pristigli si podaci " + sensor.Type + " " + sensor.Value);
            await _dataRepository.PostData(sensor);
            return Ok();
        }

        [HttpGet("{sensorType}")]
        public async Task<IEnumerable<SensorDb>> GetData([FromRoute] string sensorType)
        {
            var sensors = await _dataRepository.GetData(sensorType);
            return sensors;
        }
    }
}
