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
    public class ChildWoRelConfiguration : IEntityTypeConfiguration<ChildWoRel>
    {
        public void Configure(EntityTypeBuilder<ChildWoRel> builder)
        {
            builder
                .ToTable("ChildWoRel");
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.WoId)
                .HasColumnName("WoId");
            builder
                .Property(b => b.PartId)
                .HasColumnName("PartId");
            builder
                .Property(b => b.Qnty)
                .HasColumnName("Qnty");
            builder
                .Property(b => b.CameFrom)
                .HasColumnName("CameFrom");
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("ChildWoRel_TenantId");
        }
    }
}
