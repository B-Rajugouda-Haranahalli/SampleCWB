using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.Routings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Configurations.Routings
{
    public class RoutingStatusLogConfiguration
        : IEntityTypeConfiguration<RoutingStatusLog>
    {
        public void Configure(EntityTypeBuilder<RoutingStatusLog> builder)
        {
            builder
             .ToTable("RoutingStatusLog");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(m => m.RoutingId)
                .HasColumnName("RoutingId")
                .IsRequired();
            builder
                .Property(m => m.UpdatedDate)
                .HasColumnName("UpdatedDate")
                .IsRequired();
            builder
                .Property(t => t.UpdatedBy)
                .HasColumnName("UpdatedBy")
                .IsRequired();
            builder
               .Property(m => m.PrevStatus)
               .HasColumnName("PrevStatus")
               .IsRequired();
            builder
                .Property(m => m.ChangedStatus)
                .HasColumnName("ChangedStatus")
                .IsRequired();
            builder
               .Property(m => m.Reason)
               .HasColumnName("Reason")
               .IsRequired();
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
        }
    }
}
