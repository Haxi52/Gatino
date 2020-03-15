using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gatino.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet("a/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("a/login")]
        public IActionResult Login(Models.Account.LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);

            return Redirect("~/");
        }
    }
}