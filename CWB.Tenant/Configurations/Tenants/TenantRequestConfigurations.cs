using CWB.CommonUtils.Common.Configurations;
using CWB.Tenant.Domain.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Tenant.Configurations.Tenants
{
    public class TenantRequestConfigurations : IEntityTypeConfiguration<TenantRequest>
    {
        public void Configure(EntityTypeBuilder<TenantRequest> builder)
        {
            builder
              .ToTable("TenantRequests");
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
                .Property(t => t.RequestStatus)
                .HasConversion<string>()
                .HasColumnName("RequestStatus")
                .IsUnicode(true)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(t => t.CompanyInfo)
                .HasColumnName("CompanyInfo")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");

            builder
                .Property(t => t.Comments)
                .HasColumnName("Comments")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
        }
    }
}
