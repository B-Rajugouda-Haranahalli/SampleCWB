using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class UOMConfigurations : IEntityTypeConfiguration<UOM>
    {
        public void Configure(EntityTypeBuilder<UOM> builder)
        {
            builder
             .ToTable("UOMs");
            builder
               .HasKey(b => b.Id);
            builder
                .Property(b => b.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(b => b.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();

            builder.ConfigureBase();
            builder.HasIndex(b => b.TenantId).HasDatabaseName("UOM_TenantId");
        }
    }
}