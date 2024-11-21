using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class ManufacturedPartConfigurations : IEntityTypeConfiguration<ManufacturedPart>
    {
        public void Configure(EntityTypeBuilder<ManufacturedPart> builder)
        {
            builder
             .ToTable("ManufacturedParts");
            builder
               .HasKey(b => b.Id);
            builder
                .Property(b => b.ManufacturedPartType)
                .HasConversion<string>()
                .HasColumnName("ManufacturedPartType")
                .IsUnicode(true)
                .HasMaxLength(30)
                .IsRequired();
            builder
                .HasOne(b => b.Company)
                .WithMany(b => b.ManufacturedParts)
                .HasForeignKey(b => b.CompanyId)
                .IsRequired();
            builder
                .HasOne(b => b.UOM)
                .WithMany(b => b.ManufacturedParts)
                .HasForeignKey(b => b.UOMId)
                .IsRequired();
            builder
                .Property(b => b.FinishedWeight)
                .HasColumnName("FinishedWeight")
                .HasMaxLength(50)
                .IsRequired();
            builder
                .HasOne(b => b.MasterPart)
                .WithOne(b => b.ManufacturedPart)
                .HasForeignKey<ManufacturedPart>(b => b.MasterPartId)
                .IsRequired();
            builder
                .Property(b => b.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(b => b.TenantId).HasDatabaseName("ManufacturedPart_TenantId");
            builder.HasIndex(b => b.CompanyId).HasDatabaseName("ManufacturedPart_CompanyId");
        }
    }
}
