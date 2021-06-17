using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnalyticsMicroservice.Models;
using AnalyticsMicroservice.Repository;
using Microsoft.AspNetCore.Cors;

namespace AnalyticsMicroservice.Controllers
{
    [EnableCors("SOAPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AnalyticsController : ControllerBase
    {
        public ISensorRepository _dataRepository;
        public AnalyticsController(ISensorRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SEvent sensor)
        {
            Console.WriteLine("From AC " + sensor.Event.Timestamp);
            Sensor st = sensor.Event;
            await _dataRepository.PostData(st);
            return Ok();
        }
        [HttpGet("{sensorType}")]
        public Task<IEnumerable<SensorDb>> GetData([FromRoute] string sensorType)
        {
            var sensors = _dataRepository.GetData(sensorType);
            return sensors;
        }
    }
}