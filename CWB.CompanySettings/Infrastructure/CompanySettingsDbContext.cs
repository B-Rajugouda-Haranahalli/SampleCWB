using CWB.CommonUtils.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace CWB.CompanySettings.Infrastructure
{
    public class CompanySettingsDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CompanySettingsDbContext(DbContextOptions<CompanySettingsDbContext> options, IHttpContextAccessor httpContextAccessor)
           : base(options)
        {
            this._httpContextAccessor = httpContextAccessor;

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(CompanySettingsDbContext).Assembly);
        }

        #region Override

        public override int SaveChanges()
        {

            AddTimeNUserStamps();
            return base.SaveChanges();

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimeNUserStamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimeNUserStamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreationDate = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).Creator = string.IsNullOrEmpty(userId) ? "0" : userId;
                }
                else
                {
                    entity.Property("CreationDate").IsModified = false;
                    entity.Property("Creator").IsModified = false;
                }
                ((BaseEntity)entity.Entity).LastModifier = string.IsNullOrEmpty(userId) ? "0" : userId;
                ((BaseEntity)entity.Entity).LastModifiedDate = DateTime.UtcNow;
            }

        }
        #endregion
    }
}
