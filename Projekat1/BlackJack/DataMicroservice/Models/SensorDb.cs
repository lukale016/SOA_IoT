using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataMicroservice.Models
{
    public class SensorDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Type { get; set; }
        public string Timestamp { get; set; }
        public int Value { get; set; }
        public SensorDb(string sensorType, int value, string timeStamp = null)
        {
            Timestamp = (timeStamp == null) ? DateTime.Now.ToShortTimeString() : timeStamp;
            Value = value;
            Type = sensorType;
        }
    }
}