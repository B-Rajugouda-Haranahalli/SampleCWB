using CWB.CommonUtils.Common.Configurations;
using CWB.Simulation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Simulation.Configurations
{
    public class VendorConfigurations : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder
                 .ToTable("Vendors");
            builder.ConfigureBase();
            builder
                .Property(w => w.Name)
                .HasColumnName("Name")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(t => t.Type)
                .HasConversion<string>()
                .HasColumnName("VendorType")
                .IsUnicode(true)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();

            builder.HasIndex(m => m.TenantId).HasDatabaseName("Vendor_TenantId");
        }
    }
}
