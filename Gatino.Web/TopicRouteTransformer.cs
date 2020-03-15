using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gatino.Web
{
    public class TopicRouteTransformer : DynamicRouteValueTransformer
    {
        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            values.Add("controller", "Topic");
            values.Add("action", "index");

            var fullPath = httpContext.Request.Path.Value ?? "/";

            var path = fullPath.Substring(0, fullPath.LastIndexOf("/"));
            var page = fullPath.Substring(fullPath.LastIndexOf("/") + 1);

            if (string.IsNullOrWhiteSpace(path))
            {
                path = "/";
            }
            if (string.IsNullOrWhiteSpace(page))
            {
                page = "start";
            }

            values["path"] = path;
            values["page"] = page;

            return new ValueTask<RouteValueDictionary>(values);
        }
    }
}
