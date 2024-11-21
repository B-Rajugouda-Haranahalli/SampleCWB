using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class BoughtOutFinishDetailConfigurations : IEntityTypeConfiguration<BoughtOutFinishDetail>
    {
        public void Configure(EntityTypeBuilder<BoughtOutFinishDetail> builder)
        {
            builder
             .ToTable("BoughtOutFinishDetails");
            builder
               .HasKey(m => m.Id);

            builder
                .Property(m => m.BoughtOutFinishMadeType)
                .HasColumnName("BoughtOutFinishMadeType")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(m => m.PartId)
                .HasColumnName("PartId")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(t => t.SupplierPartNo)
                .HasConversion<string>()
                .HasColumnName("SupplierPartNo")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(t => t.AdditionalInfo)
                .HasConversion<string>()
                .HasColumnName("AdditionalInfo")
                .IsUnicode(true)
                .HasMaxLength(255);
            /*    builder
                    .Property(t => t.PartDescription)
                    .HasConversion<string>()
                    .HasColumnName("PartDescription")
                    .IsUnicode(true)
                    .HasMaxLength(255)

           /*     builder
                    .Property(t => t.Status)
                    .HasColumnName("Status")
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
                    .HasMaxLength(255);
                builder
                   .Property(t => t.PurchaseDetailId)
                   .HasColumnName("PurchaseDetailId")
                   .IsUnicode(true)
                   .HasMaxLength(255);
                builder
                    .Property(t => t.Supplier)
                    .HasConversion<string>()
                    .HasColumnName("Supplier")
                    .IsUnicode(true)
                    .HasMaxLength(255);
                builder
                    .Property(t => t.PurchaseDetail)
                    .HasConversion<string>()
                    .HasColumnName("PurchaseDetail")
                    .IsUnicode(true)
                    .HasMaxLength(255);


                builder
                    .Property(t => t.PurSupplierPartNo)
                    .HasConversion<string>()
                    .HasColumnName("PurSupplierPartNo")
                    .IsUnicode(true)
                    .HasMaxLength(255);
                builder
                    .Property(t => t.LeadTimeInDays)
                    .HasColumnName("LeadTimeInDays")
                    .IsUnicode(true)
                    .HasMaxLength(255);
                builder
                    .Property(t => t.MinOrderQuantity)
                    .HasColumnName("MinOrderQuantity")
                    .IsUnicode(true)
                    .HasMaxLength(255);
                builder
                    .Property(t => t.Price)
                    .HasColumnName("Price")
                    .IsUnicode(true)
                    .HasMaxLength(255);
                builder
                    .Property(t => t.ShareOfBusiness)
                    .HasConversion<string>()
                    .HasColumnName("ShareOfBusiness")
                    .IsUnicode(true)
                    .HasMaxLength(255);
                builder
                    .Property(t => t.PurAdditionalInformation)
                    .HasConversion<string>()
                    .HasColumnName("PurAdditionalInformation")
                    .IsUnicode(true)
                    .HasMaxLength(255);*/
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(m => m.TenantId).HasDatabaseName("BoughtOutFinishDetails_TenantId");
        }
    }
}
