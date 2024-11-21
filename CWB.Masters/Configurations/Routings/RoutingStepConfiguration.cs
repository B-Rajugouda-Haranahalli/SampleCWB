using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.Routings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class RoutingStepConfiguration
        : IEntityTypeConfiguration<RoutingStep>
    {
        /***
                 *  `Id` bigint NOT NULL AUTO_INCREMENT,
          `RoutingStepId` bigint NOT NULL,
          `ManufacturedPartId` bigint NOT NULL,/*Refers to Id from ManufactredPartNoDetails
          `BOMId` bigint not NULL, /*Refers to BOM entry from MPBOMs table
          `QuantityAssembly` int NOT NULL,
         */
        public void Configure(EntityTypeBuilder<RoutingStep> builder)
        {
            builder
             .ToTable("RoutingStep");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(m => m.RoutingId)
                .HasColumnName("RoutingId")
                .IsRequired();
            builder
              .Property(m => m.StepNumber)
              .HasColumnName("StepNumber")
              .HasMaxLength(255)
              .IsRequired();
            builder
             .Property(m => m.StepDescription)
             .HasColumnName("StepDescription")
             .HasMaxLength(300);

            builder
               .Property(t => t.RoutingStepOperation)
               .HasColumnName("RoutingStepOperation")
               .HasConversion<string>()
               .HasMaxLength(300)
               .IsRequired();
            builder
               .Property(t => t.RoutingStepLocation)
               .HasColumnName("RoutingStepLocation")
               .HasConversion<string>()
               .HasMaxLength(300)
               .IsRequired();
            builder
              .Property(t => t.Status)
              .HasColumnName("Status")
              .IsRequired();
            builder
             .Property(t => t.RoutingStepSequence)
             .HasColumnName("RoutingStepSequence")
             .IsRequired();
            builder
             .Property(t => t.NumberOfSimMachines)
             .HasColumnName("NumberOfSimMachines")
             .IsRequired();
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder
            .Property(m => m.OrigStepId)
            .HasColumnName("OrigStepId")
            .IsRequired();
            builder.ConfigureBase();
        }
    }
}
