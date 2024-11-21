using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.Routings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class RoutingStepPartConfiguration : IEntityTypeConfiguration<RoutingStepPart>
    {
        /**  `Id` bigint NOT NULL AUTO_INCREMENT,
    ->   `RoutingStepId` bigint NOT NULL,
    ->   `ManufacturedPartId` bigint NOT NULL,
    ->   `BOMId` bigint not NULL, 
    ->   `QuantityAssembly` int NOT NULL,
         */
        public void Configure(EntityTypeBuilder<RoutingStepPart> builder)
        {
            builder
             .ToTable("RoutingStepPart");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(m => m.RoutingStepId)
                .HasColumnName("RoutingStepId")
                .IsRequired();
            builder
               .Property(m => m.ManufacturedPartId)
               .HasColumnName("ManufacturedPartId")
               .IsRequired();
            builder
               .Property(m => m.BOMId)
               .HasColumnName("BOMId")
               .IsRequired();
            builder
              .Property(m => m.QuantityAssembly)
              .HasColumnName("QuantityAssembly")
              .HasMaxLength(255)
              .IsRequired();
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder
               .Property(m => m.OrigStepPartId)
               .HasColumnName("OrigStepPartId")
               .IsRequired();
            builder.ConfigureBase();
        }
    }
}
