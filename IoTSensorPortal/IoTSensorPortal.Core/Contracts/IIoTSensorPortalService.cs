using IoTSensorPortal.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTSensorPortal.Core.Contracts
{
    public interface IIoTSensorPortalService
    {
        long CreateSensor(SensorViewModel model);

        void EditSensor(SensorViewModel model, string userId);

        IEnumerable<SensorViewModel> GetMySensors(string userId);
        
        IEnumerable<SensorViewModel> GetSensors();

        IEnumerable<SensorViewModel> GetPublicSensors();
        
        SensorViewModel GetSensor(long id);

        void DeleteSensor(long sensorId, string userId);
    }
}
