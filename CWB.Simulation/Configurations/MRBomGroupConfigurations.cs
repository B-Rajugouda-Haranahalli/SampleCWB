using CWB.CommonUtils.Common.Configurations;
using CWB.Simulation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Simulation.Configurations
{
    public class MRBomGroupConfigurations : IEntityTypeConfiguration<MRBomGroup>
    {
        public void Configure(EntityTypeBuilder<MRBomGroup> builder)
        {
            builder
                .ToTable("MRBomGroups");
            builder.ConfigureBase();
            builder
                .Property(w => w.Name)
                .HasColumnName("Name")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.HasIndex(m => m.TenantId).HasDatabaseName("MRBomGroups_TenantId");
        }

    }
}
