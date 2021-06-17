using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HandThreeMicroservice.Models;
using HandThreeMicroservice.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HandThreeMicroservice.Controllers
{
    [EnableCors("SOAPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SensorController : ControllerBase
    {
        private readonly HandService _serviceHandThree;
        public SensorController()
        {
            _serviceHandThree = new HandService("card3");
        }

        [HttpGet("{type}")]
        public ActionResult<SensorMetadata> GetSensorMetadata(string type)
        {
            if (type == null)
                return BadRequest("No sensor type specified");

            if (type.ToLower() == _serviceHandThree.SensorType.ToLower())
            {
                SensorMetadata metadata = new SensorMetadata(type, _serviceHandThree.Timeout.ToString(), _serviceHandThree.Threshold.ToString());
                return metadata;
            }

            return BadRequest("Sensor type doesn't exist");
        }

        [HttpGet("{type}")]
        public IActionResult GetTimeout([Required, FromRoute] string type)
        {
            if (type.ToLower() == _serviceHandThree.SensorType.ToLower())
            {
                return new JsonResult(new {isTimeout = !_serviceHandThree.IsThresholdSet, value = _serviceHandThree.Timeout});
            }
    
            return BadRequest("Sensor type doesn't exist");
        }

        [HttpGet("{type}")]
        public IActionResult GetThreshold(string type)
        {
            if (type.ToLower() == _serviceHandThree.SensorType.ToLower())
            {
                return new JsonResult(new { isTreshold = _serviceHandThree.IsThresholdSet, value = _serviceHandThree.Threshold });
            }

            return BadRequest("Sensor type doesn't exist");
        }

        [HttpPost]
        public IActionResult TurnOnOffSensor()
        {
            if(_serviceHandThree.IsOn)
            {
                _serviceHandThree.SensorOff();
            }
            else
            {
                _serviceHandThree.SensorOn();
            }
            return Ok();
        }

        [HttpPost("{type}")]
        public IActionResult SetTimeout(
            [Required, FromRoute] string type, [Required, FromBody] double? value)
        {

            if (type.ToLower() == _serviceHandThree.SensorType.ToLower())
            {
                _serviceHandThree.IsThresholdSet = false;
                if (value != null)
                {
                    _serviceHandThree.SetTimeout((double)value);
                    return Ok($"New Timeout value set for {type} sensor");
                }
                else
                {
                    return Ok($"Last Timeout value used for {type} sensor");
                }
            }

            return BadRequest("Sensor type doesn't exist");
        }

        [HttpPost("{type}")]
        public IActionResult SetThreshold(
            [Required, FromRoute] string type, [Required, FromBody] double? value)
        {
            if (value == null) return BadRequest("Provide treshold value");


            if (type.ToLower() == _serviceHandThree.SensorType.ToLower())
            {
                _serviceHandThree.IsThresholdSet = true;
                if (value != null)
                {
                    _serviceHandThree.Threshold = (float)value;
                    return Ok($"New Threshold value set for {type} sensor");
                }
                else
                {
                    return Ok($"Default Threshold value used for {type} sensor");
                }
            }

            return BadRequest("Sensor type doesn't exist");
        }
    }
}
