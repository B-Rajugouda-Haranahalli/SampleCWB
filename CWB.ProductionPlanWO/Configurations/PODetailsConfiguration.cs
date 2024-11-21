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
    public class PODetailsConfiguration : IEntityTypeConfiguration<PODetails>
    {
        public void Configure(EntityTypeBuilder<PODetails> builder)
        {
            builder
                .ToTable("PODetails");
            builder
                .HasKey(w => w.Id);
            builder
               .Property(t => t.ProcPlanId)
               .HasColumnName("ProcPlanId")
               .IsRequired();
            builder
               .Property(t => t.POReference)
               .HasColumnName("POReference")
               .IsRequired();
            builder
               .Property(t => t.AddHocPO)
               .HasColumnName("AddHocPO");
            builder
               .Property(t => t.PartId)
               .HasColumnName("PartId")
               .IsRequired();
            builder
               .Property(t => t.PoQnty)
               .HasColumnName("PoQnty")
               .IsRequired();
            builder
               .Property(t => t.PoDate)
               .HasColumnName("PoDate")
               .IsRequired();
            builder
               .Property(t => t.CompanyId)
               .HasColumnName("CompanyId")
               .IsRequired();
            builder
               .Property(t => t.PlanPoReceiptDate)
               .HasColumnName("PlanPoReceiptDate")
               .IsRequired();
            builder
               .Property(t => t.PoSent)
               .HasColumnName("PoSent")
               .IsRequired();
            builder
               .Property(t => t.PoQntyRecd)
               .HasColumnName("PoQntyRecd")
               .IsRequired();
            builder
               .Property(t => t.Status)
               .HasColumnName("Status")
               .IsRequired();
            builder
               .Property(t => t.TenantId)
               .HasColumnName("TenantId")
               .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("PODetails_TenantId");

        }
    }
}
