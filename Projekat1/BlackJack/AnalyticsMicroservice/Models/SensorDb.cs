using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnalyticsMicroservice.Models
{
    public class SensorDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Type { get; set; }
        public string Timestamp { get; set; }
        public double Value { get; set; }
        public SensorDb(string sensorType, double value, string timeStamp = null)
        {
            Timestamp = (timeStamp == null) ? DateTime.Now.ToShortTimeString() : timeStamp;
            Value = value;
            Type = sensorType;
        }
    }
}