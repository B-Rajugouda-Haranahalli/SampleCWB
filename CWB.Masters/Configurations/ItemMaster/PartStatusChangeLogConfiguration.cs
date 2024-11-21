using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class PartStatusChangeLogConfiguration : IEntityTypeConfiguration<PartStatusChangeLog>
    {
        public void Configure(EntityTypeBuilder<PartStatusChangeLog> builder)
        {
            builder
             .ToTable("PartStatusChangeLogs");
            builder
               .HasKey(b => b.Id);
            builder
                .HasOne(b => b.MasterPart)
                .WithMany(b => b.PartStatusChangeLogs)
                .HasForeignKey(b => b.MasterPartId)
                .IsRequired();
            builder
                .Property(b => b.Status)
                .HasConversion<string>()
                .HasColumnName("Status")
                .IsUnicode(true)
                .HasMaxLength(25)
                .IsRequired();
            builder
                .Property(b => b.ChangeReason)
                .HasColumnName("ChangeReason")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
            builder
                .Property(b => b.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(b => b.TenantId).HasDatabaseName("PartStatusChangeLog_TenantId");
        }
    }
}
