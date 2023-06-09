using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Mvc.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet("/contact")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

