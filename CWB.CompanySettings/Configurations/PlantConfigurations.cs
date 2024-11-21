using CWB.CommonUtils.Common.Configurations;
using CWB.CompanySettings.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.CompanySettings.Configurations
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
            builder
                .Property(w => w.City)
                .HasColumnName("City")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(w => w.Pincode)
                .HasColumnName("Pincode")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(w => w.Country)
                .HasColumnName("Country")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(w => w.GstNo)
                .HasColumnName("GstNo")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(w => w.PanNo)
                .HasColumnName("PanNo")
                .HasMaxLength(255)
                .IsRequired();
            builder.HasIndex(m => m.TenantId).HasDatabaseName("Plant_TenantId");
        }
    }
}
