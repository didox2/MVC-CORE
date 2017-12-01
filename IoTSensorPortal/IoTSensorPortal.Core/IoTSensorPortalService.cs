using IoTSensorPortal.Core.Contracts;
using IoTSensorPortal.Core.Models;
using IoTSensorPortal.Infrastructure.Data.Contracts;
using IoTSensorPortal.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IoTSensorPortal.Core
{
    public class IoTSensorPortalService : IIoTSensorPortalService
    {
        private readonly IRDBERepository<Sensor> sensorRepository;
        private readonly IRDBERepository<History> historyRepository;
        private readonly IRDBERepository<ApplicationUser> userRepository;

        public IoTSensorPortalService(IRDBERepository<Sensor> sensorRepository,
                                        IRDBERepository<History> historyRepository,
                                        IRDBERepository<ApplicationUser> userRepository)
        {

            this.sensorRepository = sensorRepository ?? throw new ArgumentNullException("sensorRepository");
            this.historyRepository = historyRepository ?? throw new ArgumentNullException("historyRepository");
            this.userRepository = userRepository ?? throw new ArgumentNullException("userRepository");
        }

        public long CreateSensor(SensorViewModel model)
        {
            var sensor = new Sensor
            {
                CurrentValue = model.CurrentValue,
                IsPublic = model.IsPublic,
                MaxValue = model.MaxValue,
                MinValue = model.MinValue,
                Name = model.Name,
                RefreshRate = model.RefreshRate,
                Url = model.Url,
                OwnerId = model.OwnerId,
                Owner = this.userRepository.GetById(model.OwnerId)
            };

            this.sensorRepository.Add(sensor);

            this.sensorRepository.SaveChanges();

            return sensor.Id;
        }

        public void DeleteSensor(long sensorId, string userId)
        {
            var sensor = this.sensorRepository.GetById(sensorId);
            if (sensor.OwnerId == userId)
            {
                this.sensorRepository.Delete(sensorId);

                this.sensorRepository.SaveChanges();
            }
        }

        public void EditSensor(SensorViewModel model, string userId)
        {
            if (this.userRepository.GetById(userId).UserName == model.OwnerId)
            {
                var item = this.sensorRepository.GetById(model.Id);

                item.Url = model.Url;
                item.IsPublic = model.IsPublic;
                item.MaxValue = model.MaxValue;
                item.MinValue = model.MinValue;
                item.Name = model.Name;
                item.RefreshRate = model.RefreshRate;

                this.sensorRepository.SaveChanges();
            }
        }

        public IEnumerable<SensorViewModel> GetMySensors(string userId)
        {
            return this.sensorRepository.AllReadonly()
               .Where(x => x.OwnerId == userId)
               .Select(x => new SensorViewModel()
               {
                   CurrentValue = x.CurrentValue,
                   Id = x.Id,
                   IsPublic = x.IsPublic,
                   LastUpdated = x.LastUpdated,
                   MaxValue = x.MaxValue,
                   MinValue = x.MinValue,
                   Name = x.Name,
                   Owner = x.Owner,
                   OwnerId = x.OwnerId,
                   RefreshRate = x.RefreshRate,
                   Url = x.Url
               }).ToList();
        }

        public IEnumerable<SensorViewModel> GetPublicSensors()
        {
            return this.sensorRepository.AllReadonly()
                .Where(x => x.IsPublic == true)
                .Select(x => new SensorViewModel()
                {
                    CurrentValue = x.CurrentValue,
                    Id = x.Id,
                    IsPublic = x.IsPublic,
                    LastUpdated = x.LastUpdated,
                    MaxValue = x.MaxValue,
                    MinValue = x.MinValue,
                    Name = x.Name,
                    Owner = x.Owner,
                    OwnerId = x.OwnerId,
                    RefreshRate = x.RefreshRate,
                    Url = x.Url
                }).ToList();
        }

        public SensorViewModel GetSensor(long id)
        {
            var model = this.sensorRepository.GetById(id);

            var reuslt = new SensorViewModel
            {
                Id = model.Id,
                CurrentValue = model.CurrentValue,
                IsPublic = model.IsPublic,
                LastUpdated = model.LastUpdated,
                MaxValue = model.MaxValue,
                MinValue = model.MinValue,
                Name = model.Name,
                Owner = model.Owner,
                OwnerId = model.OwnerId,
                RefreshRate = model.RefreshRate,
                Url = model.Url
            };

            return reuslt;
        }

        public IEnumerable<SensorViewModel> GetSensors()
        {
            return this.sensorRepository.AllReadonly()
                .Select(t => new SensorViewModel()
                {
                    Id = t.Id,
                    CurrentValue = t.CurrentValue,
                    IsPublic = t.IsPublic,
                    LastUpdated = t.LastUpdated,
                    MaxValue = t.MaxValue,
                    MinValue = t.MinValue,
                    Name = t.Name,
                    Owner = t.Owner,
                    OwnerId = t.OwnerId,
                    RefreshRate = t.RefreshRate,
                    Url = t.Url
                });
        }
    }
}
