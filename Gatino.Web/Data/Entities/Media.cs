using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gatino.Web.Data.Entities
{
    public class Media : IAuditEntity
    {
        public Guid MediaId { get; set; }
        public string Path { get; set; }
        public string MimeType { get; set; }
        public byte[] Data { get; set; }

        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }
    }
}
