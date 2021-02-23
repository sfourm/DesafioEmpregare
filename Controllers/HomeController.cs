using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Empregare.Models;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using System.Net;
using System;
using Empregare.ViewModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Empregare.Data;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Globalization;

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

