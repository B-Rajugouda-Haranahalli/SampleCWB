using CWB.CommonUtils.Common.Configurations;
using CWB.Simulation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Simulation.Configurations
{
    public class PlantConfigurations : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> builder)
        {

            builder
                  .ToTable("Plants");
            builder.ConfigureBase();
            builder
                .Property(w => w.Name)
                .HasColumnName("Name")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder
                .Property(t => t.Address)
                .HasColumnName("Address")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
            builder
                .Property(t => t.Notes)
                .HasColumnName("Notes")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
            builder
                .Property(o => o.IsMainPlant)
                .HasColumnName("IsMainPlant")
                .IsRequired()
                .HasDefaultValue(false);
            builder
                .Property(o => o.IsProductDesigned)
                .HasColumnName("IsProductDesigned")
                .IsRequired()
                .HasDefaultValue(false);
            builder.HasIndex(m => m.TenantId).HasDatabaseName("Plant_TenantId");
        }
    }
}
