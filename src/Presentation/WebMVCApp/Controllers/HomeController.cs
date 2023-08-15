using Microsoft.AspNetCore.Mvc;
using CoreX.Web;
using CoreX.Base;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace WebMVCApp.Controllers
{
    public class HomeController : CoreXMVCController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var devError = ErrorHelper.GetDevError(HttpContext.Features.
                Get<IExceptionHandlerPathFeature>()!.Error);

            devError.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(devError);
        }
    }
}