using CWB.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CWB.Identity.DbContext
{
    public class CwbIdentityDbContext : IdentityDbContext<CwbUser>
    {
        public CwbIdentityDbContext(DbContextOptions<CwbIdentityDbContext> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CwbUser>().ToTable("CwbUsers");
            builder.Entity<IdentityRole>().ToTable("CwbRoles");
            builder.Entity<IdentityUserRole<string>>().ToTable("CwbUserRoles");
            builder.Entity<IdentityUserLogin<string>>().ToTable("CwbUserLogins");
            builder.Entity<IdentityUserClaim<string>>().ToTable("CwbUserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("CwbRoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("CwbUserToken");
            builder.ApplyConfigurationsFromAssembly(typeof(CwbIdentityDbContext).Assembly);
        }
    }
}
