using Mamba.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Mamba.MVC.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }

     
    }
}
