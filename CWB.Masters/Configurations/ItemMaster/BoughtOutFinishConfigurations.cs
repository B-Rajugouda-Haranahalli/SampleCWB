using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class BoughtOutFinishConfigurations : IEntityTypeConfiguration<BoughtOutFinish>
    {
        public void Configure(EntityTypeBuilder<BoughtOutFinish> builder)
        {
            builder
             .ToTable("BoughtOutFinishs");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(b => b.BoughtOutFinishMadeType)
                .HasConversion<string>()
                .HasColumnName("BoughtOutFinishMadeType")
                .IsUnicode(true)
                .HasMaxLength(25)
                .IsRequired();
            builder
                .Property(b => b.SupplierPartNo)
                .HasColumnName("SupplierPartNo")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(b => b.AdditionalInformation)
                .HasColumnName("AdditionalInformation")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
            builder
                .HasOne(b => b.MasterPart)
                .WithOne(b => b.BoughtOutFinish)
                .HasForeignKey<BoughtOutFinish>(b => b.MasterPartId)
                .IsRequired();
            builder
                .Property(b => b.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(b => b.TenantId).HasDatabaseName("BoughtOutFinish_TenantId");
        }
    }
}
