using IoTSensorPortal.Core.Contracts;
using IoTSensorPortal.Core.Models;
using IoTSensorPortal.Infrastructure.Data.Contracts;
using IoTSensorPortal.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISensorDataProvider dataProvider;

        public IoTSensorPortalService(IRDBERepository<Sensor> sensorRepository,
                                        IRDBERepository<History> historyRepository,
                                        IRDBERepository<ApplicationUser> userRepository,
                                        UserManager<ApplicationUser> userManager,
                                        ISensorDataProvider dataProvider)
        {

            this.sensorRepository = sensorRepository ?? throw new ArgumentNullException("sensorRepository");
            this.historyRepository = historyRepository ?? throw new ArgumentNullException("historyRepository");
            this.userRepository = userRepository ?? throw new ArgumentNullException("userRepository");
            this.userManager = userManager ?? throw new ArgumentNullException("userManager");
            this.dataProvider = dataProvider ?? throw new ArgumentNullException("dataProvider");
        }

        #region SensorMethods
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
            var user = GetApplicationUser(userId);

            bool isAdmin = IsUserAdmin(user);

            var sensor = this.sensorRepository.GetById(sensorId);

            if (sensor.OwnerId == userId || isAdmin)
            {
                this.sensorRepository.Delete(sensorId);

                this.sensorRepository.SaveChanges();
            }
        }

        public void EditSensor(SensorViewModel model, string userId)
        {
            var user = GetApplicationUser(userId);

            var isAdmin = IsUserAdmin(user);

            if ((user.UserName == model.OwnerId) || isAdmin)
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
        #endregion

        #region UserMethods

        public void EditUser(UserViewModel model)
        {
            var applicationUser = GetApplicationUser(model.Id);

            applicationUser.Email = model.Email;
            applicationUser.UserName = model.Username;

            if (model.IsAdmin == true)
            {
                this.userManager.AddToRoleAsync(applicationUser, "Admin").Wait();
            }

            else
            {
                this.userManager.RemoveFromRoleAsync(applicationUser, "Admin").Wait();
            }

            //this.userRepository.SaveChanges();

            //this.userManager.UpdateAsync(applicationUser);
        }

        public UserViewModel GetUser(string id)
        {
            var user = this.userRepository.GetById(id);
            bool isAdmin = IsUserAdmin(user);

            var item = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = isAdmin,
                Username = user.UserName
            };

            return item;
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = this.userRepository.AllReadonly();
            var result = new List<UserViewModel>();
            bool isAdmin = false;

            foreach (var user in users)
            {
                isAdmin = IsUserAdmin(user);

                result.Add(new UserViewModel()
                {
                    Email = user.Email,
                    Id = user.Id,
                    IsAdmin = isAdmin,
                    Username = user.UserName
                });
            }

            return result;
        }

        public void DeleteUser(string userId)
        {
            var user = GetApplicationUser(userId);

            this.userRepository.Delete(user);

            this.userRepository.SaveChanges();
        }

        private ApplicationUser GetApplicationUser(string userId)
        {
            return this.userRepository.GetById(userId);
        }

        private bool IsUserAdmin(ApplicationUser user)
        {
            return this.userManager.IsInRoleAsync(user, "Admin").Result;
        }

        #endregion

        #region Provider

        public void Update()
        {
            this.dataProvider.Update();
        }

        public IEnumerable<T> GetAllSensorsInfo<T>()
        {
            return this.dataProvider.GetAllSensorsInfo<T>();
        }

        #endregion
    }
}
