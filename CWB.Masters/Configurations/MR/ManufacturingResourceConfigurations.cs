using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.MR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.MR
{
    public class ManufacturingResourceConfigurations : IEntityTypeConfiguration<ManufacturingResource>
    {
        public void Configure(EntityTypeBuilder<ManufacturingResource> builder)
        {

            builder
             .ToTable("ManufacturingResources");
            builder
               .HasKey(c => c.Id);
            builder
                .HasOne(m => m.ManufacturingResourceGroup)
                .WithMany(m => m.ManufacturingResources)
                .HasForeignKey(m => m.ManufacturingResourceGroupId)
                .IsRequired();
            builder
               .Property(t => t.MRItemType)
               .HasConversion<string>()
               .HasColumnName("MRItemType")
               .IsUnicode(true)
               .HasMaxLength(25)
               .IsRequired();
            builder
              .Property(t => t.MRConsumptionType)
              .HasConversion<string>()
              .HasColumnName("MRConsumptionType")
              .IsUnicode(true)
              .HasMaxLength(25)
              .IsRequired();
            builder
                .HasOne(b => b.UOM)
                .WithMany(b => b.ManufacturingResources)
                .HasForeignKey(b => b.UOMId)
                .IsRequired();
            builder
                .Property(t => t.ItemDescription)
                .HasColumnName("ItemDescription")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
            builder
               .Property(m => m.StockLevel)
               .HasColumnName("StockLevel")
               .HasMaxLength(255)
               .IsRequired();
            builder
              .Property(c => c.IsPartsSpecificMRItem)
              .HasColumnName("IsPartsSpecificMRItem")
              .IsRequired()
              .HasDefaultValue(false);
            builder
                .Property(c => c.ReorderLevel)
                .HasColumnName("ReorderLevel")
                .IsRequired();
            builder
               .Property(c => c.NoOfTimesCanBeReused)
               .HasColumnName("NoOfTimesCanBeReused")
               .IsRequired();
            builder
              .Property(m => m.MRItemPartNo)
              .HasColumnName("MRItemPartNo")
              .HasMaxLength(255)
              .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("ManufacturingResource_TenantId");
            builder.HasIndex(c => c.ManufacturingResourceGroupId).HasDatabaseName("ManufacturingResource_ManufacturingResourceGroupId");
        }
    }
}
