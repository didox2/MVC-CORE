using System;
using System.Collections.Generic;
using System.Text;

namespace IoTSensorPortal.Core.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}
