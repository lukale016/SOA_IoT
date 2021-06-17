using System.Collections.Generic;
using System.Threading.Tasks;
using DataMicroservice.Models;

namespace DataMicroservice.Repository
{
    public interface ISensorRepository
    {
        Task<IEnumerable<SensorDb>> GetData(string sensorType);
        Task PostData(Sensor sensor);
    }
}