using CWB.CommonUtils.Common.Configurations;
using CWB.Simulation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Simulation.Configurations
{
    public class MachineTypeConfigurations : IEntityTypeConfiguration<MachineType>
    {
        public void Configure(EntityTypeBuilder<MachineType> builder)
        {
            builder
             .ToTable("MachineTypes");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(m => m.Type)
                .HasColumnName("Type")
                .HasMaxLength(255)
                .IsRequired();
            builder
               .Property(m => m.TenantId)
               .HasColumnName("TenantId")
               .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(m => m.TenantId).HasDatabaseName("MachineType_TenantId");
        }
    }
}
