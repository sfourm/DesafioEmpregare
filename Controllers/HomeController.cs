using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Empregare.Controllers
{
    public class HomeController : Controller
    {

        /// Metódo construtor 
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

    }
}

