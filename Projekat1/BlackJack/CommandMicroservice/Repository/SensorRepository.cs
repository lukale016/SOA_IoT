using System.Collections.Generic;
using System.Threading.Tasks;
using CommandMicroservice.Models;
using CommandMicroservice.Services;
using MongoDB.Driver;

namespace CommandMicroservice.Repository
{
    public class SensorRepository : ISensorRepository
    {
        private readonly SensorContext _context;
        private readonly CommandService _service;

        public SensorRepository(SensorContext context, CommandService serv)
        {
            _context = context;
            _service = serv;
        }
        public async Task PostData(SensorCommand sensorDataCommand)
        {
            await _context.SensorCommand.InsertOneAsync(sensorDataCommand);
        }
        public async Task<IEnumerable<SensorCommand>> GetData(string sensorType)
        {
            FilterDefinition<SensorCommand> filter = Builders<SensorCommand>.Filter.Eq(p => p.Sensor.Type, sensorType);
            return await _context.SensorCommand.Find(filter).ToListAsync();
        }
    }
}