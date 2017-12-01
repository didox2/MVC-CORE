using System;

namespace IoTSensorPortal.Infrastructure.Data.Models
{
    public class History
    {
        public History()
        {
        }

        public long Id { get; set; }

        public long SensorId { get; set; }

        public DateTime UpdateDate { get; set; }

        public string Value { get; set; }

        public virtual Sensor Sensor { get; set; }
    }
}
