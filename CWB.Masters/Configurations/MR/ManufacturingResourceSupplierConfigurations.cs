using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.MR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.MR
{
    public class ManufacturingResourceSupplierConfigurations : IEntityTypeConfiguration<ManufacturingResourceSupplier>
    {
        public void Configure(EntityTypeBuilder<ManufacturingResourceSupplier> builder)
        {
            builder
             .ToTable("ManufacturingResourceSuppliers");
            builder
               .HasKey(c => c.Id);
            builder
                .HasOne(c => c.ManufacturingResource)
                .WithMany(c => c.ManufacturingResourceSuppliers)
                .HasForeignKey(c => c.ManufacturingResourceId)
                .IsRequired();
            builder
                .HasOne(c => c.Company)
                .WithMany(c => c.ManufacturingResourceSuppliers)
                .HasForeignKey(c => c.SupplierId)
                .IsRequired();
            builder
              .Property(m => m.DeliveryTime)
              .HasColumnName("DeliveryTime")
              .HasMaxLength(255)
              .IsRequired();
            builder
              .Property(m => m.Cost)
              .HasColumnName("Cost")
              .HasMaxLength(255)
              .IsRequired();
            builder
             .Property(m => m.MOQ)
             .HasColumnName("MOQ")
             .HasMaxLength(255)
             .IsRequired();
            builder
                .Property(t => t.Notes)
                .HasColumnName("Notes")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("ManufacturingResourceSupplier_TenantId");
            builder.HasIndex(c => c.SupplierId).HasDatabaseName("ManufacturingResourceSupplier_SupplierId");
            builder.HasIndex(c => c.ManufacturingResourceId).HasDatabaseName("ManufacturingResourceSupplier_ManufacturingResourceId");
        }
    }
}
