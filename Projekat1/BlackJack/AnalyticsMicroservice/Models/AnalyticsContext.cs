using MongoDB.Driver;

namespace AnalyticsMicroservice.Models
{
    public class AnalyticsContext
    {
        public IMongoCollection<SensorDb> SensorData { get; }
        
        public AnalyticsContext()
        {
            var client = new MongoClient("mongodb://mongodb:27017");
            var database = client.GetDatabase("SensorDb");
            SensorData = database.GetCollection<SensorDb>("SensorAnalytics");
        }
    }
}