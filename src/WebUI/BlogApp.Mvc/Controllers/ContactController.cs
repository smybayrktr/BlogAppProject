using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Mvc.Controllers
{
    public class ConcatController : Controller
    {
        [HttpGet("/concat-index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

