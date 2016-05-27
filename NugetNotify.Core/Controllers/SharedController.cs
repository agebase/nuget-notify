using System.Web.Mvc;
using Microsoft.ApplicationInsights.Extensibility;

namespace NugetNotify.Core.Controllers
{
    public class SharedController : Controller
    {
        [ChildActionOnly]
        public ActionResult ApplicationInsights()
        {
            if (HttpContext.IsDebuggingEnabled)
                return Content(string.Empty);

            var key = TelemetryConfiguration.Active.InstrumentationKey;
            if (string.IsNullOrWhiteSpace(key))
                return Content(string.Empty);

            return PartialView("ApplicationInsights", key);
        }
    }
}