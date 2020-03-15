using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gatino.Web.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public DbSet<Entities.Topic> Topics { get; set; }


        public DbContext(DbContextOptions options) : base(options)
        {
            httpContextAccessor =
                this.GetService<Microsoft.AspNetCore.Http.IHttpContextAccessor>()
                ?? throw new InvalidOperationException();

            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelMapper.Map(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            FillAuditColumnsOnSave();
            return base.SaveChangesAsync(cancellationToken);
        }


        private void FillAuditColumnsOnSave()
        {
            if (!ChangeTracker.HasChanges()) return;

            var transactionTimestamp = DateTime.UtcNow;
            var currentUser = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";

            foreach (var auditable in ChangeTracker.Entries<Data.IAuditEntity>())
            {
                if (auditable.State == EntityState.Added)
                {
                    auditable.Entity.CreatedBy = currentUser;
                    auditable.Entity.LastModifiedBy = currentUser;
                    auditable.Entity.CreatedOn = transactionTimestamp;
                    auditable.Entity.LastModifiedOn = transactionTimestamp;
                }
                else if (auditable.State == EntityState.Modified)
                {
                    auditable.Entity.LastModifiedBy = currentUser;
                    auditable.Entity.LastModifiedOn = transactionTimestamp;
                }
            }
        }
    }
}
