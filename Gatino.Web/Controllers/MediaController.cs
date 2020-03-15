using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gatino.Web.Controllers
{
    public class MediaController : Controller
    {
        public MediaController()
        {

        }

        [HttpPost]
        [Route("/m/Upload")]
        public async Task<IActionResult> Upload(List<IFormFile> files, string path)
        {
            return Json(new { link = "path/to/image.jpg" });
        }
    }
}
