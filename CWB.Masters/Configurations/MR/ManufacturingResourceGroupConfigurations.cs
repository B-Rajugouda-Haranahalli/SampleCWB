using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.MR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.MR
{
    public class ManufacturingResourceGroupConfigurations : IEntityTypeConfiguration<ManufacturingResourceGroup>
    {
        public void Configure(EntityTypeBuilder<ManufacturingResourceGroup> builder)
        {
            builder
             .ToTable("ManufacturingResourceGroups");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(c => c.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("ManufacturingResourceGroup_TenantId");
        }
    }
}
