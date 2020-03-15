using Gatino.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gatino.Web.Controllers
{
    public class MediaController : Controller
    {
        private readonly IDbContext context;

        public MediaController(Data.IDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost]
        [Route("/m/Upload")]
        public async Task<IActionResult> Upload(IFormFile file, string path)
        {
            // we always assume the file is new
            var media = new Data.Entities.Media()
            {
                MediaId = Guid.NewGuid(),
                MimeType = file.ContentType,
                Path = path,
            };
            
            using var reader = new System.IO.BinaryReader(file.OpenReadStream());
            media.Data = reader.ReadBytes((int)file.Length);

            context.Media.Add(media);
            await context.SaveChangesAsync();

            return Json(new { link = Url.Action(nameof(Get), "Media", new { mediaId = media.MediaId }) });
        }

        [HttpGet]
        [Route("/m/{mediaId}")]
        public async Task<IActionResult> Get(Guid mediaId)
        {
            var media = await context.Media
                .FirstOrDefaultAsync(i => i.MediaId == mediaId);

            if (media is null)
            {
                return NotFound();
            }

            return File(media.Data, media.MimeType);
        }
    }
}
