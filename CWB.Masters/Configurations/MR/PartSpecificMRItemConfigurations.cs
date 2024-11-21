using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.MR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class PartSpecificMRItemConfigurations : IEntityTypeConfiguration<PartSpecificMRItem>
    {
        public void Configure(EntityTypeBuilder<PartSpecificMRItem> builder)
        {
            builder
             .ToTable("PartSpecificMRItems");
            builder
               .HasKey(c => c.Id);
            builder
                .HasOne(c => c.ManufacturingResource)
                .WithMany(c => c.PartSpecificMRItems)
                .HasForeignKey(c => c.ManufacturingResourceId)
                .IsRequired();
            builder
                .Property(c => c.PartId)
                .HasColumnName("PartId")
                .IsRequired();
            builder
             .Property(m => m.Notes)
             .HasColumnName("Notes")
             .HasMaxLength(255)
             .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("PartSpecificMRItem_TenantId");
            builder.HasIndex(c => c.ManufacturingResourceId).HasDatabaseName("PartSpecificMRItem_ManufacturingResourceId");
        }
    }
}