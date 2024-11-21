using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class MasterPartConfigurations : IEntityTypeConfiguration<MasterPart>
    {
        public void Configure(EntityTypeBuilder<MasterPart> builder)
        {
            builder
             .ToTable("MasterParts");
            builder
               .HasKey(b => b.Id);
            builder
                .Property(b => b.PartNo)
                .HasColumnName("PartNo")
                .HasMaxLength(255)
                .IsRequired();
            builder
              .Property(b => b.PartDescription)
              .HasColumnName("PartDescription")
              .IsUnicode(true)
              .HasMaxLength(4000)
              .HasColumnType("nvarchar(MAX)");
            builder
               .Property(b => b.MasterPartType)
               .HasConversion<string>()
               .HasColumnName("MasterPartType")
               .IsUnicode(true)
               .HasMaxLength(25)
               .IsRequired();
            builder
                .Property(b => b.Status)
                .HasConversion<string>()
                .HasColumnName("Status")
                .IsUnicode(true)
                .HasMaxLength(25)
                .IsRequired();
            builder
              .Property(b => b.StatusChangeReason)
              .HasConversion<string>()
              .HasColumnName("StatusChangeReason")
              .IsUnicode(true)
              .HasMaxLength(300);
            builder
               .Property(b => b.RevNo)
                .HasConversion<string>()
               .HasColumnName("RevNo")
               .HasMaxLength(255);
            builder
                .Property(b => b.RevDate)
                .HasColumnName("RevDate")
                .HasColumnType("datetime");
            builder
                .Property(b => b.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(b => b.TenantId).HasDatabaseName("MasterPart_TenantId");
            builder.HasIndex(b => b.MasterPartType).HasDatabaseName("MasterPart_MasterPartType");

        }
    }
}
