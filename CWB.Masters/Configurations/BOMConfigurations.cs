using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class BOMConfigurations : IEntityTypeConfiguration<MPBOM>
    {
        public void Configure(EntityTypeBuilder<MPBOM> builder)
        {
            builder
            .ToTable("MPBOMs");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.PartId)
                .HasColumnName("PartId")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(t => t.PartDesc)
                .HasColumnName("PartDescription")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .IsRequired();
            builder
                .Property(t => t.Quantity)
                .HasColumnName("Quantity")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(c => c.ManufPartId)
                .HasColumnName("ManufPartId")
                .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("MPBOM_TenantId");
        }
    }
}
