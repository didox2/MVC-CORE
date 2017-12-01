using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IoTSensorPortal.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.MySensors = new HashSet<Sensor>();
            this.SharedSensors = new HashSet<UserSensor>();
        }

        public virtual ICollection<Sensor> MySensors { get; set; }

        public virtual ICollection<UserSensor> SharedSensors { get; set; }

    }
}
