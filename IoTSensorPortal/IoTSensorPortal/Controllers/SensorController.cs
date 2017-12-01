using IoTSensorPortal.Core.Contracts;
using IoTSensorPortal.Core.Models;
using IoTSensorPortal.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IoTSensorPortal.Controllers
{
    public class SensorController : Controller
    {
        private readonly IIoTSensorPortalService service;
        private readonly UserManager<ApplicationUser> userManager;

        public SensorController(IIoTSensorPortalService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service ?? throw new ArgumentNullException("service");
            this.userManager = userManager ?? throw new ArgumentNullException("userManager");
        }

        [Authorize]
        public ActionResult CreateSensor()
        {
            return this.View();
        }

        [Authorize, ValidateAntiForgeryToken, HttpPost]
        public ActionResult CreateSensor(SensorViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                model.OwnerId = this.userManager.FindByNameAsync(this.User.Identity.Name).Result.Id;
                this.service.CreateSensor(model);
                return this.RedirectToAction("MySensors");
            };

            return this.View();
        }

        [Authorize]
        public ActionResult EditSensor()
        {
            return this.View();
        }

        [Authorize, ValidateAntiForgeryToken, HttpPost]
        public ActionResult EditSensor(SensorViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditSensor(model);
                return this.RedirectToAction("MySensors");
            };

            return this.View();
        }

        [Authorize]
        public ActionResult Details(long id)
        {
            var model = this.service.GetSensor(id);

            return this.View(model);
        }

        public ActionResult AllPublicSensors()
        {
            var models = this.service.GetPublicSensors();

            return this.View(models);
        }

        [Authorize]
        public ActionResult MySensors()
        {
            var userId = this.userManager.FindByNameAsync(this.User.Identity.Name).Result.Id;
            var models = this.service.GetMySensors(userId);

            return this.View(models);
        }

    }
}