using CWB.CommonUtils.Common.Configurations;
using CWB.ProductionPlanWO.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.ProductionPlanWO.Configurations
{
    public class ProcPlanConfigurations : IEntityTypeConfiguration<ProcPlan>
    {
        public void Configure(EntityTypeBuilder<ProcPlan> builder)
        {
            builder
                .ToTable("ProcPlan");
            builder
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Reference)
                .HasColumnName("Reference")
                .IsRequired();
            builder
                .Property(p => p.TestData)
                .HasColumnName("TestData")
                .HasDefaultValue(0);
            builder
                .Property(p => p.For_Ref)
                .HasColumnName("For_Ref")
                .HasDefaultValue(0);
            builder
                .Property(p => p.WorkOrderId)
                .HasColumnName("WorkOrderId")
                .IsRequired();
            builder
                .Property(p => p.PartId)
                .HasColumnName("PartId")
                .IsRequired();
            builder
                .Property(p => p.PartType)
                .HasColumnName("PartType")
                .HasDefaultValue(0);
            builder
                .Property(p => p.UOMId)
                .HasColumnName("UOMId")
                .HasDefaultValue(0);
            builder
                .Property(p => p.PartType)
                .HasColumnName("PartType")
                .HasDefaultValue(0);
            builder
                .Property(p => p.Calc_Proc_Qnty)
                .HasColumnName("Calc_Proc_Qnty")
                .HasDefaultValue(0);
            builder
                .Property(p => p.QtyOnHand)
                .HasColumnName("QtyOnHand")
                .HasDefaultValue(0);
            builder
                .Property(p => p.QtyOnHand)
                .HasColumnName("QtyOnHand")
                .HasDefaultValue(0);
            builder
                .Property(p => p.AddnOtyUser)
                .HasColumnName("AddnOtyUser")
                .HasDefaultValue(0);
            builder
                .Property(p => p.Plan_Proc_Qnty)
                .HasColumnName("Plan_Proc_Qnty")
                .HasDefaultValue(0);
            builder
                .Property(p => p.PlanReceiptDate)
                .HasColumnName("PlanReceiptDate")
                .HasDefaultValue(null);
            builder
                .Property(p => p.CalcReceiptDate)
                .HasColumnName("CalcReceiptDate")
                .HasDefaultValue(null);
            builder
                .Property(p => p.CriticalPart)
                .HasColumnName("CriticalPart")
                .HasDefaultValue(0);
            builder
                .Property(p => p.Changed)
                .HasColumnName("Changed")
                .HasDefaultValue(0);
            builder
                .Property(p => p.TenantId)
                .HasColumnName("TenantId")
                .HasDefaultValue(0);
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("ProcPlan_TenantId");

        }
    }
}
