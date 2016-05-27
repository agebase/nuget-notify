using System.Web.Mvc;
using NugetNotify.Core.Services;
using NugetNotify.Core.ViewModels;

namespace NugetNotify.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPackageNotificationService _packageNotificationService;

        public HomeController(IPackageNotificationService packageNotificationService)
        {
            _packageNotificationService = packageNotificationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Submit()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Submit(HomeIndexViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", model);

            _packageNotificationService.Create(
                model.PackageName,
                model.Email,
                model.Twitter,
                model.Telephone);

            return RedirectToAction("Thanks");
        }

        public ActionResult Thanks()
        {
            return View();
        }
    }
}