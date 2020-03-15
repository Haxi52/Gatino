using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gatino.Web.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet("/a/Users")]
        public IActionResult Users()
        {
            return View();
        }
    }
}