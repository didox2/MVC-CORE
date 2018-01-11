using IoTSensorPortal.Infrastructure.Data.Models;
using IoTSensorPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace IoTSensorPortal.Controllers
{
    public class HomeController : Controller
    {
        //private readonly RoleManager<IdentityRole> roleManager;
        //private readonly UserManager<ApplicationUser> userManager;
        //public HomeController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        //{
        //    this.roleManager = roleManager;
        //    this.userManager = userManager;
        //}
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            //roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            //userManager.AddToRoleAsync(userManager.Users.First(), "Admin").Wait();

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
