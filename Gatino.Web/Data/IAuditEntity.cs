using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gatino.Web.Data
{
    public interface IAuditEntity
    {
        string CreatedBy { get; set; }
        DateTimeOffset CreatedOn { get; set; }
        string LastModifiedBy { get; set; }
        DateTimeOffset LastModifiedOn { get; set; }
    }
}
