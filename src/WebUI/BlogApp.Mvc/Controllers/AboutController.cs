using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Mvc.Controllers
{
    public class AboutController : Controller
    {
        [HttpGet("/about")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

