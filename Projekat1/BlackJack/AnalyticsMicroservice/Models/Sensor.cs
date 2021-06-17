using System;

namespace AnalyticsMicroservice.Models
{
    public class Sensor
    {
        
        public string Type { get; set; }
        public int Value { get; set; }
        public string Timestamp { get; set; }

        public Sensor(string type, int value, string timestamp = null)
        {
            Type = type;
            Value = value;
            Timestamp = (timestamp == null) ? DateTime.Now.ToShortTimeString() : timestamp;
        }
    }
}