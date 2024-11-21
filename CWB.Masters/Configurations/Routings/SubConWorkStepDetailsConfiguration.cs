using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.Routings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class SubConWorkStepDetailsConfiguration
        : IEntityTypeConfiguration<SubConWorkStepDetails>
    {
        public void Configure(EntityTypeBuilder<SubConWorkStepDetails> builder)
        {
            builder
             .ToTable("SubConWorkStepDetails");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(m => m.RoutingStepId)
                .HasColumnName("RoutingStepId")
                .IsRequired();
            builder
                .Property(t => t.SubConDetailsId)
                .HasColumnName("SubConDetailsId")
                .IsRequired();
            builder
               .Property(m => m.WorkStepDesc)
               .HasColumnName("WorkStepDesc")
               .HasMaxLength(300)
               .IsRequired();
            builder
               .Property(m => m.MachineType)
               .HasColumnName("MachineType")
               .IsRequired();
            builder
              .Property(m => m.SetupTime)
              .HasColumnName("SetupTime")
              .IsRequired();
            builder
             .Property(m => m.FloorToFloorTime)
             .HasColumnName("FloorToFloorTime")
             .IsRequired();
            builder
            .Property(m => m.NoOfPartsPerLoading)
            .HasColumnName("NoOfPartsPerLoading")
            .IsRequired();
            builder
            .Property(m => m.OrigSubConWSId)
            .HasColumnName("OrigSubConWSId")
            .IsRequired();
            builder
            .Property(m => m.TenantId)
            .HasColumnName("TenantId")
            .IsRequired();
            builder.ConfigureBase();
        }
    }
}
