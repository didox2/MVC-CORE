using IoTSensorPortal.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IoTSensorPortal.Core.Models
{
    public class SensorViewModel
    {
        public long Id { get; set; }
        
        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public string Url { get; set; }

        //public IEnumerable<Urls> Urls { get; set; }
        
        public string Name { get; set; }

        [Display(Name = "Refresh Rate")]
        public int RefreshRate { get; set; }

        [Display(Name = "Min Value")]
        public int MinValue { get; set; }

        [Display(Name = "Max Value")]
        public int MaxValue { get; set; }

        [Display(Name = "Is Sensor Public")]
        public bool IsPublic { get; set; }

        [Display(Name = "Current Value")]
        public string CurrentValue { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }

        //public ICollection<ApplicationUser> SharedWithUsers { get; set; }

        //public ICollection<History> History { get; set; }
    }

    //public class Urls
    //{
    //    Guid SensorId { get; set; }

    //    string Tag { get; set; }

    //    string Description { get; set; }

    //    int MinPoolingIntervalInSeconds { get; set; }

    //    string MeasureType { get; set; }

    //    //"sensorId":"f1796a28-642e-401f-8129-fd7465417061",
    //    //"tag":"TemperatureSensor1",
    //    //"description":"This sensor will return values between 15 and 28",
    //    //"minPollingIntervalInSeconds":40,
    //    //"measureType":"°C"
    //}
}
