using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.Routings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class RoutingConfiguration
        : IEntityTypeConfiguration<Routing>
    {
        public void Configure(EntityTypeBuilder<Routing> builder)
        {
            builder
             .ToTable("Routing");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(m => m.RoutingName)
                .HasColumnName("RoutingName")
                .HasMaxLength(300)
                .IsRequired();
            builder
                .Property(t => t.ManufacturedPartId)
                .HasColumnName("ManufacturedPartId")
                .IsRequired();
            builder
               .Property(m => m.OrigRoutingId)
               .HasColumnName("OrigRoutingId")
               .IsRequired();
            builder
               .Property(m => m.PreferredRouting)
               .HasColumnName("PreferredRouting")
               .IsRequired();
            builder
                .Property(m => m.MKPartId)
                .HasColumnName("MKPartId")
                .IsRequired();
            builder
              .Property(m => m.Status)
              .HasColumnName("Status")
              .IsRequired();
            builder
              .Property(m => m.StatusChangeReason)
              .HasColumnName("StatusChangeReason")
                .HasMaxLength(300)
              .HasDefaultValue("");
            builder
              .Property(m => m.Deleted)
              .HasColumnName("Deleted")
              .IsRequired();
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
        }
    }
}
