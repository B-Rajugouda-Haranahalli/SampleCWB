using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class RawMaterialDetailConfigurations : IEntityTypeConfiguration<RawMaterialDetail>
    {
        public void Configure(EntityTypeBuilder<RawMaterialDetail> builder)
        {
            builder
             .ToTable("RawMaterialDetails");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(t => t.PartId)
                .HasColumnName("PartId")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();
            builder
             .Property(t => t.SupplierId)
             .HasColumnName("SupplierId")
             .IsUnicode(true)
             .HasMaxLength(255)
             .IsRequired();
            builder
                .Property(t => t.UOMId)
                .HasColumnName("UOMId")
                .HasMaxLength(255);
            builder
                .Property(m => m.RawMaterialMadeType)
                .HasColumnName("RawMaterialMadeType")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(m => m.RawMaterialMadeSubType)
                .HasColumnName("RawMaterialMadeSubType")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(t => t.RawMaterialTypeId)
                .HasColumnName("RawMaterialTypeId")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();

            builder
                 .Property(t => t.BaseRawMaterialId)
                 .HasColumnName("BaseRawMaterialId")
                 .HasMaxLength(255);
            builder
                 .Property(t => t.RawMaterialWeight)
                 .HasColumnName("RawMaterialWeight")
                 .HasMaxLength(255);
            builder
                 .Property(t => t.RawMaterialNotes)
                 .HasConversion<string>()
                 .HasColumnName("RawMaterialNotes")
                 .IsUnicode(true)
                 .HasMaxLength(255);
            builder
              .Property(t => t.Standard)
              .HasColumnName("Standard")
              .HasMaxLength(255);
            builder
                .Property(t => t.MaterialSpecId)
                .HasColumnName("MaterialSpecId")
                .HasMaxLength(255);

            builder
                    .Property(t => t.ReorderLevel)
                    .HasColumnName("ReorderLevel")
                    .HasMaxLength(255);

            builder
                .Property(t => t.ReorderQnty)
                .HasColumnName("ReorderQnty")
                .HasDefaultValue(0);
            builder
                .Property(t => t.TimetoDeliverReorderQnty)
                .HasColumnName("TimetoDeliverReorderQnty")
                .HasDefaultValue(0);

            /**
             * builder
               .Property(t => t.PartDescription)
               .HasConversion<string>()
               .HasColumnName("PartDescription")
               .IsUnicode(true)
               .HasMaxLength(255);
            builder
                .Property(t => t.Status)
                .HasColumnName("Status")
                .IsUnicode(true)
                .HasMaxLength(255);
            builder
                .Property(t => t.StatusChangeReason)
                .HasConversion<string>()
                .HasColumnName("StatusChangeReason")
                .IsUnicode(true)
                .HasMaxLength(255);
            builder
                .Property(t => t.RevNo)
                .HasConversion<string>()
                .HasColumnName("RevNo")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(t => t.RevDate)
                .HasColumnName("RevDate")
                .IsUnicode(true)
                .HasMaxLength(255);*/

            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(m => m.TenantId).HasDatabaseName("RawMaterialDetails_TenantId");
        }
    }
}
