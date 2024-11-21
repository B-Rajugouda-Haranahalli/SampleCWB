using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class RawMaterialConfigurations : IEntityTypeConfiguration<RawMaterial>
    {
        public void Configure(EntityTypeBuilder<RawMaterial> builder)
        {
            builder
             .ToTable("RawMaterials");
            builder
               .HasKey(b => b.Id);
            builder
                .HasOne(b => b.MasterPart)
                .WithOne(b => b.RawMaterial)
                .HasForeignKey<RawMaterial>(b => b.MasterPartId)
                .IsRequired();
            builder
                .Property(b => b.RawMaterialMadeType)
                .HasConversion<string>()
                .HasColumnName("RawMaterialMadeType")
                .IsUnicode(true)
                .HasMaxLength(25)
                .IsRequired();
            builder
                .HasOne(b => b.RawMaterialType)
                .WithMany(b => b.RawMaterials)
                .HasForeignKey(b => b.RawMaterialTypeId)
                .IsRequired();
            builder
                .HasOne(b => b.BaseRawMaterial)
                .WithMany(b => b.RawMaterials)
                .HasForeignKey(b => b.BaseRawMaterialId)
                .IsRequired();
            builder
                .Property(b => b.Weight)
                .HasColumnName("Weight")
                .HasMaxLength(25)
                .IsRequired();
            builder
                .Property(b => b.Notes)
                .HasColumnName("Notes")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
            builder
                .HasOne(b => b.RawMaterialStandard)
                .WithMany(b => b.RawMaterials)
                .HasForeignKey(b => b.RawMaterialStandardId)
                .IsRequired();
            builder
                .HasOne(b => b.RawMaterialSpec)
                .WithMany(b => b.RawMaterials)
                .HasForeignKey(b => b.RawMaterialSpecId)
                .IsRequired();
            builder
                .Property(b => b.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(b => b.TenantId).HasDatabaseName("RawMaterial_TenantId");
            builder.HasIndex(b => b.RawMaterialTypeId).HasDatabaseName("RawMaterial_RawMaterialTypeId");
            builder.HasIndex(b => b.BaseRawMaterialId).HasDatabaseName("RawMaterial_BaseRawMaterialId");
        }
    }
}
