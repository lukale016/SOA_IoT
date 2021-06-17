using MongoDB.Driver;

namespace CommandMicroservice.Models
{
    public class SensorContext
    {
        public readonly IMongoCollection<SensorCommand> SensorCommand;

        public SensorContext()
        {
            var client = new MongoClient("mongodb://mongodb:27017");
            var database = client.GetDatabase("SensorDb");
            SensorCommand = database.GetCollection<SensorCommand>("SensorCommand");
        }
    }
}