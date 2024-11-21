using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.Routings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class RoutingStepSupplierConfiguration : IEntityTypeConfiguration<RoutingStepSupplier>
    {
        
        public void Configure(EntityTypeBuilder<RoutingStepSupplier> builder)
        {
            builder
             .ToTable("RoutingStepSupplier");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(m => m.RoutingStepId)
                .HasColumnName("RoutingStepId")
                .IsRequired();
            builder
               .Property(m => m.SupplierId)
               .HasColumnName("SupplierId")
               .IsRequired();
            builder
               .Property(m => m.OutSourceDays)
               .HasColumnName("OutSourceDays")
               .IsRequired();
            builder
              .Property(m => m.ShareOfBusiness)
              .HasColumnName("ShareOfBusiness")
              .HasMaxLength(255)
              .IsRequired();
            builder
            .Property(m => m.Notes)
            .HasColumnName("Notes")
            .HasMaxLength(255)
            .IsRequired();
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
        }
    }
}
