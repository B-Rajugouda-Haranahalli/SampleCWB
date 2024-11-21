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
    public class McTimeListConfiguration : IEntityTypeConfiguration<McTimeList>
    {
        public void Configure(EntityTypeBuilder<McTimeList> builder)
        {
            builder
               .ToTable("McTimeList");
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.WoId)
                .HasColumnName("WoId");
            builder
                .Property(b => b.Routing_StepId)
                .HasColumnName("Routing_StepId");
            builder
                .Property(b => b.CompanyId)
                .HasColumnName("CompanyId");
            builder
                .Property(b => b.MachineId)
                .HasColumnName("MachineId");
            builder
                .Property(b => b.MachineTypeId)
                .HasColumnName("MachineTypeId");
            builder
                .Property(b => b.PlanQnty)
                .HasColumnName("PlanQnty");
            builder
                .Property(b => b.TotalPlanTime)
                .HasColumnName("TotalPlanTime");
            builder
                .Property(b => b.McPlanStartTime)
                .HasColumnName("McPlanStartTime");
            builder
                .Property(b => b.McPlanEndTime)
                .HasColumnName("McPlanEndTime");
            builder
                .Property(b => b.McActStartTime)
                .HasColumnName("McActStartTime");
            builder
                .Property(b => b.McActEndTime)
                .HasColumnName("McActEndTime");
            builder
                .Property(b => b.ActQnty)
                .HasColumnName("ActQnty");
            builder
                .Property(b => b.TotalActTime)
                .HasColumnName("TotalActTime");
            builder
                .Property(b => b.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("McTimeList_TenantId");
        }
    }
}
