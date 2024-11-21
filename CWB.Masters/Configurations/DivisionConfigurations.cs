using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class DivisionConfigurations : IEntityTypeConfiguration<Division>
    {
        public void Configure(EntityTypeBuilder<Division> builder)
        {
            builder
             .ToTable("Divisions");
            builder
               .HasKey(m => m.Id);

            builder
                .Property(m => m.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(m => m.Location)
                .HasColumnName("Location")
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(t => t.Notes)
                .HasColumnName("Notes")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
            builder
                .Property(m => m.PlantName)
                .HasColumnName("PlantName")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(m => m.City)
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
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder
                .HasOne(m => m.Company)
                .WithMany(m => m.Divisions)
                .HasForeignKey(m => m.CompanyId)
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(m => m.TenantId).HasDatabaseName("Division_TenantId");
            builder.HasIndex(m => m.CompanyId).HasDatabaseName("Division_CompanyId");
        }
    }
}
