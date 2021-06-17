using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HandOneAndTwoMicroservice.Models;
using HandOneAndTwoMicroservice.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HandOneAndTwoMicroservice.Controllers
{
    [EnableCors("SOAPolicy")]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SensorController : ControllerBase
    {
     private readonly HandService _serviceHandOne;
     private readonly HandService _serviceHandTwo;
        public SensorController()
        {
            _serviceHandOne = new HandService("card1");
            _serviceHandTwo = new HandService("card2");
        }

        [HttpGet("{type}")]
        public ActionResult<SensorMetadata> GetSensorMetadata(string type)
        {
            if (type == null)
                return BadRequest("No sensor type specified");

            if (type.ToLower() == _serviceHandOne.SensorType.ToLower())
            {
                SensorMetadata metadata = new SensorMetadata(type, _serviceHandOne.Timeout.ToString(), _serviceHandOne.Threshold.ToString());
                return metadata;
            }
            if (type.ToLower() == _serviceHandTwo.SensorType.ToLower())
            {
                SensorMetadata metadata = new SensorMetadata(type, _serviceHandTwo.Timeout.ToString(), _serviceHandTwo.Threshold.ToString());
                return metadata;
            }

            return BadRequest("Sensor type doesn't exist");
        }

        [HttpGet("{type}")]
        public IActionResult GetTimeout([Required, FromRoute] string type)
        {
            if (type.ToLower() == _serviceHandOne.SensorType.ToLower())
            {
                return new JsonResult(new {isTimeout = !_serviceHandOne.IsThresholdSet, value = _serviceHandOne.Timeout});
            }

            if (type.ToLower() == _serviceHandTwo.SensorType.ToLower())
            {
                return new JsonResult(new {isTimeout = !_serviceHandTwo.IsThresholdSet, value = _serviceHandTwo.Timeout});
            }

            return BadRequest("Sensor type doesn't exist");
        }

        [HttpGet("{type}")]
        public IActionResult GetThreshold(string type)
        {
            if (type.ToLower() == _serviceHandOne.SensorType.ToLower())
            {
                return new JsonResult(new { isTreshold = _serviceHandOne.IsThresholdSet, value = _serviceHandOne.Threshold });
            }

            if (type.ToLower() == _serviceHandTwo.SensorType.ToLower())
            {
                return new JsonResult(new { isTreshold = _serviceHandTwo.IsThresholdSet, value = _serviceHandTwo.Threshold });
            }

            return BadRequest("Sensor type doesn't exist");
        }

        [HttpPost]
        public IActionResult TurnOnOffSensor()
        {
            if(_serviceHandOne.IsOn)
            {
                _serviceHandOne.SensorOff();
                _serviceHandTwo.SensorOff();
            }
            else
            {
                _serviceHandOne.SensorOn();
                _serviceHandTwo.SensorOn();
            }
            return Ok();
        }

        [HttpPost("{type}")]
        public IActionResult SetTimeout(
            [Required, FromRoute] string type, [Required, FromBody] double? value)
        {

            if (type.ToLower() == _serviceHandOne.SensorType.ToLower())
            {
                _serviceHandOne.IsThresholdSet = false;
                if (value != null)
                {
                    _serviceHandOne.SetTimeout((double)value);
                    return Ok($"New Timeout value set for {type} sensor");
                }
                else
                {
                    return Ok($"Last Timeout value used for {type} sensor");
                }
            }

            if (type.ToLower() == _serviceHandTwo.SensorType.ToLower())
            {
                _serviceHandTwo.IsThresholdSet = false;
                if (value != null)
                {
                    _serviceHandTwo.SetTimeout((double)value);
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
            if (value == null) return BadRequest("Threshold value is null");


            if (type.ToLower() == _serviceHandOne.SensorType.ToLower())
            {
                _serviceHandOne.IsThresholdSet = true;
                if (value != null)
                {
                    _serviceHandOne.Threshold = (float)value;
                    return Ok($"New Threshold value set for {type} sensor");
                }
                else
                {
                    return Ok($"Default Threshold value used for {type} sensor");
                }
            }

            if (type.ToLower() == _serviceHandTwo.SensorType.ToLower())
            {
                _serviceHandTwo.IsThresholdSet = true;
                if (value != null)
                {
                    _serviceHandTwo.Threshold = (float)value;
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
