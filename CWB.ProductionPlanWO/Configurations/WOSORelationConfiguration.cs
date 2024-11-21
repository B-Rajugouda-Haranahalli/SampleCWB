using CWB.ProductionPlanWO.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Configurations
{
    public class WOSORelationConfiguration : IEntityTypeConfiguration<WOSO>
    {
        public void Configure(EntityTypeBuilder<WOSO> builder)
        {
            builder
                .ToTable("WOSORel");
            builder
                .HasKey(w => w.Id);
            builder
              .Property(t => t.WorkOrderId)
              .HasColumnName("WorkOrderId")
              .IsRequired();
            builder
               .Property(t => t.SalesOrderId)
               .HasColumnName("SalesOrderId")
               .IsRequired();
            builder
               .Property(t => t.Active)
               .HasColumnName("Active")
               .IsRequired();

        }

    }
}
