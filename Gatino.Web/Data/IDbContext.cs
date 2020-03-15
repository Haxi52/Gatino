
using Gatino.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Gatino.Web.Data
{
    public interface IDbContext
    {
        DbSet<Topic> Topics { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}