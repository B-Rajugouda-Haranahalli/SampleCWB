using CWB.CommonUtils.Common.Configurations;
using CWB.Simulation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Simulation.Configurations
{
    public class BomConfigurations : IEntityTypeConfiguration<Bom>
    {
        public void Configure(EntityTypeBuilder<Bom> builder)
        {
            builder
             .ToTable("Boms");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(w => w.Quantity)
                .HasColumnName("Quantity")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(m => m.ItemMasterId).HasDatabaseName("Bom_ItemMasterId");
            builder.HasIndex(m => m.TenantId).HasDatabaseName("Bom_TenantId");
        }
    }
}
