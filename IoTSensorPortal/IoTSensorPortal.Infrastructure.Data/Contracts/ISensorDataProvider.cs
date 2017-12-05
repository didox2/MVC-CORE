using IoTSensorPortal.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTSensorPortal.Infrastructure.Data.Contracts
{
    public interface ISensorDataProvider
    {
        IEnumerable<T> GetAllSensorsInfo<T>();

        History GetRealTimeValue(string URL);

        void Update();
    }
}
