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
    public class BOMTempConfiguration : IEntityTypeConfiguration<BOMTemp>
    {
        public void Configure(EntityTypeBuilder<BOMTemp> builder)
        {
            builder
                .ToTable("BOMTemp");
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.WorkOrderId)
                .HasColumnName("WorkOrderId");
            builder
                .Property(b => b.PartId)
                .HasColumnName("PartId");
            builder
                .Property(b => b.PartType)
                .HasColumnName("PartType");
            builder
                .Property(b => b.Parentlevel)
                .HasColumnName("Parentlevel");
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("BOMTemp_TenantId");
        }
    }
}
