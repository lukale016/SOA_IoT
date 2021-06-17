using System.Collections.Generic;
using System.Threading.Tasks;
using AnalyticsMicroservice.Models;
using AnalyticsMicroservice.Services;
using MongoDB.Driver;

namespace AnalyticsMicroservice.Repository
{
    public class SensorRepository : ISensorRepository
    {
        private readonly AnalyticsContext _context;
        private readonly AnalyticsService _service;

        public SensorRepository(AnalyticsContext context, AnalyticsService serv)
        {
            _context = context;
            _service = serv;
        }
        public async Task<IEnumerable<SensorDb>> GetData(string sensorType)
        {
            FilterDefinition<SensorDb> filter = Builders<SensorDb>.Filter.Eq(p => p.Type, sensorType);
            return await _context.SensorData.Find(filter).ToListAsync();
        }
        public async Task PostData(Sensor sensor)
        {
            SensorDb vl = new SensorDb(sensor.Type, sensor.Value, sensor.Timestamp);
            await _context.SensorData.InsertOneAsync(vl);
            
             _service.PublishOnTopic(sensor, "sensor/analytics");
        }
    }
}