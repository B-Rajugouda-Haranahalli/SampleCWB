using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class ManufacturedPartBOMConfigurations : IEntityTypeConfiguration<ManufacturedPartBOM>
    {
        public void Configure(EntityTypeBuilder<ManufacturedPartBOM> builder)
        {
            builder
             .ToTable("ManufacturedPartBOMs");
            builder
               .HasKey(b => b.Id);
            builder
                .Property(b => b.BOMQuantity)
                .HasColumnName("BOMQuantity")
                .IsRequired();
            builder
                .Property(b => b.PartId)
                .HasColumnName("PartId")
                .IsRequired();
            builder
                .HasOne(b => b.ManufacturedPart)
                .WithMany(b => b.ManufacturedPartBOMs)
                .HasForeignKey(b => b.ManufacturedPartId)
                .IsRequired();
            builder
                .Property(b => b.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(b => b.ManufacturedPartId).HasDatabaseName("ManufacturedPartBOM_ManufacturedPartId");
            builder.HasIndex(b => b.TenantId).HasDatabaseName("ManufacturedPartBOM_TenantId");
        }
    }
}
