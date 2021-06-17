namespace CommandMicroservice.Models
{
    public class SensorCommand
    {
        public Sensor Sensor { get; set; }
        public string Command { get; set; }
        public SensorCommand(string type, int value, string command)
        {
            Sensor = new Sensor() {
                Value = value,
                Type = type
            };
            Command = command;
        }
    }
}