using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IoTSensorPortal.Infrastructure.Data.Models
{
    public class Sensor
    {
        public Sensor()
        {
            this.SharedWithUsers = new HashSet<UserSensor>();
            this.History = new HashSet<History>();
        }

        public long Id { get; set; }
        
        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public int RefreshRate { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public bool IsPublic { get; set; }

        public string CurrentValue { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual ICollection<UserSensor> SharedWithUsers { get; set; }

        public virtual ICollection<History> History { get; set; }
    }
}
