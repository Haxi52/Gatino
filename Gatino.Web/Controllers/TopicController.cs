using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gatino.Web.Controllers
{
    public class TopicController : Controller
    {
        private readonly Data.IDbContext context;

        public TopicController(Data.IDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IActionResult> Index(string page, string path)
        {
            var topic = await context.Topics
                .FirstOrDefaultAsync(i => i.Path == path && i.Name == page);


            if (topic is null)
            {
                return View(new Models.Topic.TopicModel()
                {
                    Name = page,
                    Path = path,
                    Data = null,
                });
            }

            return View(new Models.Topic.TopicModel()
            {
                Name = topic.Name,
                Path = topic.Path,
                LastModifiedBy = topic.LastModifiedBy,
                LastModifiedOn = topic.LastModifiedOn,
                Data= topic.Data
            });
        }

        [HttpPost]
        public async Task<IActionResult> Index(Models.Topic.TopicModel model)
        {
            var topic = await context.Topics
                .FirstOrDefaultAsync(i => i.Path == model.Path && i.Name == model.Name);

            if (topic is null)
            {
                topic = new Data.Entities.Topic()
                {
                    Name = model.Name,
                    Path = model.Path,
                };
                context.Topics.Add(topic);
            }

            topic.Data = model.Data;

            await context.SaveChangesAsync();

            return Redirect($"{model.Path}{model.Name}");
        }
    }
}