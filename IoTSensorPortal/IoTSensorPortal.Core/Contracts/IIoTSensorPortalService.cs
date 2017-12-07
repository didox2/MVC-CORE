using IoTSensorPortal.Core.Models;
using System.Collections.Generic;

namespace IoTSensorPortal.Core.Contracts
{
    public interface IIoTSensorPortalService
    {
        void Update();

        IEnumerable<T> GetAllSensorsInfo<T>();
        
        long CreateSensor(SensorViewModel model);

        void EditSensor(SensorViewModel model, string userId);

        IEnumerable<SensorViewModel> GetMySensors(string userId);
        
        IEnumerable<SensorViewModel> GetSensors();

        IEnumerable<SensorViewModel> GetPublicSensors();
        
        SensorViewModel GetSensor(long id);

        void DeleteSensor(long sensorId, string userId);

        UserViewModel GetUser(string id);

        IEnumerable<UserViewModel> GetUsers();

        void EditUser(UserViewModel model);

        void DeleteUser(string id);
    }
}
