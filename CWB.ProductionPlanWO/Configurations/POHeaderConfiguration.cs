using CWB.CommonUtils.Common.Configurations;
using CWB.ProductionPlanWO.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Configurations
{
    public class POHeaderConfiguration : IEntityTypeConfiguration<POHeader>
    {
        public void Configure(EntityTypeBuilder<POHeader> builder)
        {
            builder
                .ToTable("POHeader");
            builder
                .HasKey(w => w.Id);
            builder
               .Property(t => t.PoDetailsId)
               .HasColumnName("PoDetailsId")
               .IsRequired();
            builder
               .Property(t => t.SupplierId)
               .HasColumnName("SupplierId")
               .IsRequired();
            builder
               .Property(t => t.PartId)
               .HasColumnName("PartId")
               .IsRequired();
            builder
               .Property(t => t.TenantId)
               .HasColumnName("TenantId")
               .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("POHeader_TenantId");

        }
    }
}
