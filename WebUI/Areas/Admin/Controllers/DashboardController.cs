using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using WebUI.Service;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin, Moderator")]
    public class DashboardController : Controller
    {
        private readonly IService service;
        private readonly IService service2;

        public DashboardController(IService service, IService service2)
        {
            this.service = service;
            this.service2 = service2;
        }

        public IActionResult Index()
        {

            ViewBag.Data = service.Name;
            ViewBag.Data2 = service2.Name;
            return View();
        }
    }
}
