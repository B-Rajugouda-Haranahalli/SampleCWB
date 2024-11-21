using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class MachineTypeConfigurations : IEntityTypeConfiguration<MachineType>
    {
        public void Configure(EntityTypeBuilder<MachineType> builder)
        {
            builder
           .ToTable("MachineTypes");
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
            builder.HasIndex(c => c.TenantId).HasDatabaseName("MachineType_TenantId");
        }
    }
}
