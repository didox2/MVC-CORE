using IoTSensorPortal.Infrastructure.Data.Contracts;
using IoTSensorPortal.Infrastructure.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IoTSensorPortal.Infrastructure.Data.Providers
{
    public class SensorDataProvider : ISensorDataProvider
    {
        private readonly SensorHttpClient client;
        private readonly IRDBERepository<Sensor> sensorRepository;

        public SensorDataProvider(IRDBERepository<Sensor> sensorRepository)
        {
            this.sensorRepository = sensorRepository ?? throw new ArgumentNullException("sensorRepository");
            this.client = new SensorHttpClient();
        }

        public IEnumerable<T> GetAllSensorsInfo<T>()
        {
            var sensorsInfo = new List<T>();

            var response = this.client.GetAsync($"api/sensor/all").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                sensorsInfo = JsonConvert.DeserializeObject<List<T>>(result);
            }

            return sensorsInfo;
        }

        public History GetRealTimeValue(string URL)
        {
            var response = this.client.GetAsync($"api/sensor/{URL}").Result;

            History sensorState = new History();

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                sensorState = JsonConvert.DeserializeObject<History>(result);
            }
            sensorState.UpdateDate = DateTime.Now;

            return sensorState;
        }

        public void Update()
        {
            var sensors = this.sensorRepository.All();

            foreach (var sensor in sensors)
            {
                if (DateTime.Now - sensor.LastUpdated >= TimeSpan.FromSeconds(sensor.RefreshRate))
                {
                    var newState = GetRealTimeValue(sensor.Url);

                    newState.SensorId = sensor.Id;
                    newState.Sensor = sensor;
                    sensor.History.Add(newState);
                    sensor.LastUpdated = newState.UpdateDate;
                    sensor.CurrentValue = newState.Value;
                }
            }

            sensorRepository.SaveChanges();
        }
    }
}
