using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataMicroservice.Models;
using DataMicroservice.Services;
using MongoDB.Driver;

namespace DataMicroservice.Repository
{
    public class SensorRepository : ISensorRepository 
    {
        private readonly DataContext _context;
        private readonly DataService _service;

        public SensorRepository(DataContext context, DataService serv)
        {
            _context = context;
            _service = serv;
        }
        public async Task PostData(Sensor sensor)
        {    
            SensorDb vl = new SensorDb(sensor.Type, sensor.Value, sensor.Timestamp);
            await _context.SensorData.InsertOneAsync(vl);
             _service.PublishOnTopic(sensor, "sensor/data");
        }
        public async Task<IEnumerable<SensorDb>> GetData(string sensorType)
        {
            FilterDefinition<SensorDb> filter = Builders<SensorDb>.Filter.Eq(p => p.Type, sensorType);
            return await _context.SensorData.Find(filter).ToListAsync();
        }
    }
}