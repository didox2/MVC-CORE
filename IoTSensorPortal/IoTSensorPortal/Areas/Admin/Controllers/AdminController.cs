using IoTSensorPortal.Core.Contracts;
using IoTSensorPortal.Core.Models;
using IoTSensorPortal.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IoTSensorPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IIoTSensorPortalService service;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(IIoTSensorPortalService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service ?? throw new ArgumentNullException("service");
            this.userManager = userManager ?? throw new ArgumentNullException("userManager");
        }

        #region Sensors

        public IActionResult EditSensors()
        {
            var sensors = this.service.GetSensors();

            return View(sensors);
        }

        public ActionResult EditSensor(long id)
        {
            var sensor = this.service.GetSensor(id);

            return this.View(sensor);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult EditSensor(SensorViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.userManager.GetUserId(this.User);
                this.service.EditSensor(model, userId);
                return this.RedirectToAction("EditSensors");
            };

            return this.View();
        }
        
        public IActionResult DeleteSensor(long id)
        {
            this.service.DeleteSensor(id, this.User.Identity.Name);

            return RedirectToAction("EditSensors");
        }

        public ActionResult SensorDetails(long id)
        {
            var model = this.service.GetSensor(id);

            return this.View(model);
        }

        #endregion

        #region Users

        public ActionResult EditUsers()
        {
            var model = this.service.GetUsers();

            return this.View(model);
        }

        public ActionResult EditUser(string id)
        {
            var user = this.service.GetUser(id);

            return this.View(user);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult EditUser(UserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditUser(model);

                return this.RedirectToAction("EditUsers");
            };

            return this.View();
        }

        public ActionResult DeleteUser(string id)
        {
            this.service.DeleteUser(id);

            return RedirectToAction("EditUsers");
        }
        
        public ActionResult UserDetails(string id)
        {
            var model = this.service.GetUser(id);

            return this.View(model);
        }

        #endregion
    }
}