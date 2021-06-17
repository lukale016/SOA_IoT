namespace HandOneAndTwoMicroservice.Models
{
    public class SensorMetadata
    {
        public string Type { get; set; }
        public string Timeout { get; set; }
        public string Treshold { get; set; }

        public SensorMetadata(string type, string timeout, string treshold)
        {
            this.Type = type;
            this.Timeout = timeout;
            this.Treshold = treshold;
        }
    }
}