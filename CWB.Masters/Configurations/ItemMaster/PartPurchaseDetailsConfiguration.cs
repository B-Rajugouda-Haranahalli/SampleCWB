using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class PartPurchaseDetailsConfiguration : IEntityTypeConfiguration<PartPurchaseDetails>
    {
        public void Configure(EntityTypeBuilder<PartPurchaseDetails> builder)
        {
            builder
             .ToTable("PartPurchaseDetails");
            builder
               .HasKey(b => b.Id);
            builder
               .Property(b => b.PartId)
               .HasColumnName("PartId")
               .HasMaxLength(255)
               .IsRequired();
            builder
                .Property(b => b.SupplierId)
                .HasColumnName("SupplierId")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(b => b.Supplier)
                .HasColumnName("Supplier")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(b => b.SupplierPartNo)
                .HasColumnName("SupplierPartNo")
                .HasMaxLength(255)
                .IsRequired();
            builder
               .Property(b => b.LeadTimeInDays)
               .HasColumnName("LeadTimeInDays")
               .IsRequired();
            builder
               .Property(b => b.MinimumOrderQuantity)
               .HasColumnName("MinimumOrderQuantity")
               .IsRequired();
            builder
               .Property(b => b.Price)
               .HasColumnName("Price")
               .HasMaxLength(20)
               .IsRequired();
            builder
                .Property(b => b.ShareOfBusiness)
                .HasColumnName("ShareOfBusiness")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(b => b.AdditionalInfo)
                .HasColumnName("AdditionalInfo")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
            builder
               .Property(b => b.PreferredSupplier)
               .HasColumnName("PreferredSupplier")
               .HasDefaultValue(0);
            builder
               .Property(b => b.BOFId)
               .HasColumnName("BOFId")
               .HasMaxLength(255)
               .IsRequired();
            builder
              .Property(b => b.RMId)
              .HasColumnName("RMId")
              .HasMaxLength(255)
              .IsRequired();
            builder
             .Property(b => b.MasterPartType)
             .HasColumnName("MasterPartType")
             .HasMaxLength(25)
             .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(b => b.TenantId).HasDatabaseName("PartPurchaseDetail_TenantId");
        }
    }
}
