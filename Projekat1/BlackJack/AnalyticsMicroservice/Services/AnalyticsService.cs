using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AnalyticsMicroservice.Models;
using MQTTnet;

namespace AnalyticsMicroservice.Services
{
    public class AnalyticsService
    {
        private Hivemq _mqttService;
        private event EventHandler ServiceCreated;
        public AnalyticsService(Hivemq mqttService)
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
                await _mqttService.Subscribe("sensor/data", OnDataReceived);
            }
        }
        private async void OnDataReceived(MqttApplicationMessageReceivedEventArgs arg)
        {
            try
            {
                var bds = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);
                Console.WriteLine(bds);
                var des = System.Text.Json.JsonSerializer.Deserialize<Sensor>(bds);
                var options = new JsonSerializerOptions{};
                HttpClient httpClient = new HttpClient();
                var responseMessage = await httpClient.PostAsJsonAsync<Sensor>("http://192.168.1.200:8006/SiddhiMicroservice", des, options);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public async void PublishOnTopic(object data, string topic)
        {
            if(_mqttService.IsConnected())
            {
                await _mqttService.Publish(data, topic);
            }
        }
    }
}