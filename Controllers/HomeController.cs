using Microsoft.AspNetCore.Mvc;
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

namespace Empregare.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        internal static object ReferenceEquals(Func<IActionResult> index)
        {
            throw new NotImplementedException();
        }
    }
}

