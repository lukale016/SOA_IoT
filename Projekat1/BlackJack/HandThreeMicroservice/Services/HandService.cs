using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Timers;
using CsvHelper;
using CsvHelper.Configuration;
using HandThreeMicroservice.Models;

namespace HandThreeMicroservice.Services
{
    public class HandService
    {
        public const float DEFAULT_THRESHOLD = 17;
        public float Threshold { get; set; }
        public bool IsThresholdSet { get; set; }
        public double Timeout { get; set; }
        public bool IsOn { get; set; }
        public string SensorType { get; set; }
        public string _filePath;

        private readonly Timer _timer;
        private StreamReader _streamReader;
        private CsvReader _csv;

        public HandService(string sensorType)
        {
            Threshold = DEFAULT_THRESHOLD;
            Timeout = 10000;
            _timer = new Timer(Timeout);
            _timer.Elapsed += OnTimerEvent;
            SensorType = sensorType;
            _filePath = Constants.csvPath;
            _timer.Start();
            IsOn = false;
            setCsv();
            IsThresholdSet = false;
        }

        private void setCsv()
        {
            _streamReader = new StreamReader(_filePath);
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture);
            _csv = new CsvReader(_streamReader, config);
            _csv.Read();
            _csv.ReadHeader();
        }

        public void SensorOff()
        {
            IsOn = false;
            _timer.Stop();
        }

        public void SensorOn()
        {
            IsOn = true;
            _timer.Start();
        }       

        private async void OnTimerEvent(object sender, ElapsedEventArgs args)
        {
            Sensor sensor = await Task.Run(() => ReadValue());
            HttpClient httpClient = new HttpClient();
            var responseMessage = await httpClient.PostAsJsonAsync(Constants.detaMicroUrl, sensor);
        }

        public void SetTimeout(double interval)
        {
            _timer.Stop();
            Timeout = interval;
            _timer.Interval = interval;
            _timer.Start();
        }


        private Sensor ReadValue()
        {
            try
            {
                string sensorValue;
                if (_csv.Read())
                    sensorValue = _csv.GetField<string>(SensorType);
                else
                {
                    _streamReader.DiscardBufferedData();
                    setCsv();
                    _csv.Read();
                    sensorValue = _csv.GetField<string>(SensorType);
                }
                return new Sensor(SensorType, int.Parse(sensorValue, CultureInfo.InstalledUICulture));
            }
            catch (IOException e)
            {
                Console.WriteLine("This file couldn't be read: ");
                Console.WriteLine(e.StackTrace);
            }
            return null;
        }
    }
}