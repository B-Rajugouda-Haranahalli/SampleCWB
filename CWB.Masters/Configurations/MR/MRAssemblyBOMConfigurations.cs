using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.MR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.MR
{
    public class MRAssemblyBOMConfigurations : IEntityTypeConfiguration<MRAssemblyBOM>
    {
        public void Configure(EntityTypeBuilder<MRAssemblyBOM> builder)
        {
            builder
             .ToTable("MRAssemblyBOMs");
            builder
               .HasKey(c => c.Id);
            builder
                .HasOne(c => c.ManufacturingResource)
                .WithMany(c => c.MRAssemblyBOMs)
                .HasForeignKey(c => c.ManufacturingResourceId)
                .IsRequired();
            builder
                .Property(c => c.PartId)
                .HasColumnName("PartId")
                .IsRequired();
            builder
                .Property(c => c.Quantity)
                .HasColumnName("Quantity")
                .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("MRAssemblyBOM_TenantId");
            builder.HasIndex(c => c.ManufacturingResourceId).HasDatabaseName("MRAssemblyBOM_ManufacturingResourceId");
        }
    }
}
