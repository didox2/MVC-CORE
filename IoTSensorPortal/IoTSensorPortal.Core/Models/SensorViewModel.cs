using IoTSensorPortal.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTSensorPortal.Core.Models
{
    public class SensorViewModel
    {
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

        //public ICollection<ApplicationUser> SharedWithUsers { get; set; }

        //public ICollection<History> History { get; set; }
    }
}
