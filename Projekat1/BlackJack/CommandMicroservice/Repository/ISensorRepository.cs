using System.Collections.Generic;
using System.Threading.Tasks;
using CommandMicroservice.Models;

namespace CommandMicroservice.Repository
{
    public interface ISensorRepository
    {
        public Task PostData(SensorCommand sensorDataCommand);
        public Task<IEnumerable<SensorCommand>> GetData(string sensorType);
    }
}