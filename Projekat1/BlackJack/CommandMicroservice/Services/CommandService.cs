using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using CommandMicroservice.Models;
using CommandMicroservice.Repository;
using MQTTnet;
using Newtonsoft.Json;

namespace CommandMicroservice.Services
{
    public class CommandService
    {
        private Hivemq _mqttService;
        private event EventHandler ServiceCreated;
        public CommandService(Hivemq mqttService)
        {
            _mqttService = mqttService;
            ServiceCreated += OnServiceCreated;
            ServiceCreated?.Invoke(this, EventArgs.Empty);
        }
        private async void OnServiceCreated(object sender, EventArgs args)
        {
            while (!_mqttService.IsConnected())
            {
                await _mqttService.Connect();
            }
            if (_mqttService.IsConnected())
            {
                await _mqttService.Subscribe("sensor/analytics", OnDataReceived);
            }
        }

        private async void OnDataReceived(MqttApplicationMessageReceivedEventArgs arg)
        {
            try
            {
                var bds = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);
                var des = System.Text.Json.JsonSerializer.Deserialize<Sensor>(bds);
                Console.WriteLine(bds);
                Console.WriteLine(des.ToString());
                await this.SendToSensorsAsyncStop(des);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async System.Threading.Tasks.Task SendToSensorsAsyncStop(Sensor sensorData)
        {

            HttpClient httpClient = new HttpClient();
            if (sensorData.Type == "card3")
            {
                var responseMessage = await httpClient.PostAsJsonAsync("http://HandThree/api/Data/PostStop", sensorData);
            }
            else if (sensorData.Type == "card1" || sensorData.Type == "card2")
            {
                var responseMessage = await httpClient.PostAsJsonAsync("http://HandOneAndTwo/api/Data/PostStop", sensorData);
            }
            
        }
    }
}