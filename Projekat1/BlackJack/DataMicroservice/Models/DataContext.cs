using MongoDB.Driver;

namespace DataMicroservice.Models
{
    public class DataContext
    {
        public readonly IMongoCollection<SensorDb> SensorData;

        public DataContext()
        {
            var client = new MongoClient("mongodb://mongodb:27017");
            var database = client.GetDatabase("SensorDb");
            SensorData = database.GetCollection<SensorDb>("SensorReceivedData");
        }
    }
}