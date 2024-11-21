using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class RawMaterialTypeConfigurations : IEntityTypeConfiguration<RawMaterialType>
    {
        public void Configure(EntityTypeBuilder<RawMaterialType> builder)
        {
            builder
             .ToTable("RawMaterialTypes");
            builder
               .HasKey(b => b.Id);
            builder
                .Property(b => b.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(b => b.MultiplePartsMadeFrom1InputRM)
                .HasColumnName("MultiplePartsMadeFrom1InputRM")
                .HasDefaultValue('N');
            builder
                .Property(b => b.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(b => b.TenantId).HasDatabaseName("RawMaterialType_TenantId");
        }
    }
}