using CWB.CommonUtils.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Tenant.Configurations.Tenants
{
    public class TenantConfigurations : IEntityTypeConfiguration<Domain.Tenants.Tenant>
    {
        public void Configure(EntityTypeBuilder<Domain.Tenants.Tenant> builder)
        {
            builder
              .ToTable("Tenants");
            builder.ConfigureBase();
            builder
                .Property(t => t.CompanyName)
                .HasColumnName("CompanyName")
                .IsUnicode(true)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(t => t.Email)
                .HasColumnName("Email")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(t => t.Phone)
                .HasColumnName("Phone")
                .IsUnicode(true)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(o => o.IsActive)
                .HasColumnName("IsActive")
                .IsRequired()
                .HasDefaultValue(false);


            builder
                .Property(t => t.CompanyInfo)
                .HasColumnName("CompanyInfo")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");

        }
    }
}
