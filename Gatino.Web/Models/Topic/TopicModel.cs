using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gatino.Web.Models.Topic
{
    public class TopicModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Data { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }
    }
}
