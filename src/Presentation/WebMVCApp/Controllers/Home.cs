using Microsoft.AspNetCore.Mvc;
using XSwift.Mvc;
using XSwift.Base;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace Module.Presentation.WebMVCApp.Controllers
{
    public class Home : XMvcController
    {
        private readonly ILogger<Home> _logger;

        public Home(ILogger<Home> logger)
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