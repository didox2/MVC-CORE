using System;

namespace IoTSensorPortal.Infrastructure.Data.Models
{
    public class UserSensor
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public long SensorId { get; set; }
        public Sensor Sensor { get; set; }
    }
}
