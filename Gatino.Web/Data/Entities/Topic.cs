using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gatino.Web.Data.Entities
{
    public class Topic : IAuditEntity
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Data { get; set; }
        

        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }
    }
}
