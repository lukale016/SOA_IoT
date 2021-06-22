using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataMicroservice.Hubs;
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

        private DataHub _hub;
        public DataController(ISensorRepository dataRepository, DataHub hub)
        {
            _dataRepository = dataRepository;
            _hub = hub;
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Sensor sensor)
        {
            Console.WriteLine("Pristigli su podaci " + sensor.Type + " " + sensor.Value + " " + sensor.Timestamp);
            await _hub.SendData(sensor.Type, sensor.Value, sensor.Timestamp);
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
