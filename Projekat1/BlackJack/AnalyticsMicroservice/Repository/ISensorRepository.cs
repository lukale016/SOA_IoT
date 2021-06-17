using System.Collections.Generic;
using System.Threading.Tasks;
using AnalyticsMicroservice.Models;

namespace AnalyticsMicroservice.Repository
{
    public interface ISensorRepository
    {
         
        Task<IEnumerable<SensorDb>> GetData(string sensorType);
        Task PostData(Sensor sensor);
    }
}