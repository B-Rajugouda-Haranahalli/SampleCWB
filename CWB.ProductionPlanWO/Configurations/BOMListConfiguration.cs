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
    public class BOMListConfiguration : IEntityTypeConfiguration<BOMList>
    {
        public void Configure(EntityTypeBuilder<BOMList> builder)
        {
            builder
                .ToTable("BOMList");
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.ParentWoId)
                .HasColumnName("ParentWoId");
            builder
                .Property(b => b.Child_Part_No_ID)
                .HasColumnName("Child_Part_No_ID");
            builder
                .Property(b => b.Child_Part_No_Type)
                .HasColumnName("Child_Part_No_Type");
            builder
                .Property(b => b.Manf_RM_Link_ID)
                .HasColumnName("Manf_RM_Link_ID");
            builder
                .Property(b => b.Calc_Qnty)
                .HasColumnName("Calc_Qnty");
            builder
                .Property(b => b.QtyOnHand)
                .HasColumnName("QtyOnHand");
            builder
                .Property(b => b.Plan_Qnty)
                .HasColumnName("Plan_Qnty");
            builder
                .Property(b => b.Plan_Start_Dt)
                .HasColumnName("Plan_Start_Dt");
            builder
                .Property(b => b.PlanReceiptDate)
                .HasColumnName("PlanReceiptDate");
            builder
                .Property(b => b.CalcReceiptDate)
                .HasColumnName("CalcReceiptDate");
            builder
                .Property(b => b.Manf_Days_Avl)
                .HasColumnName("Manf_Days_Avl");
            builder
                .Property(b => b.Manf_Days_Reqd)
                .HasColumnName("Manf_Days_Reqd");
            builder
                .Property(b => b.CriticalPart)
                .HasColumnName("CriticalPart");
            builder
                .Property(b => b.AddnQtyUser)
                .HasColumnName("AddnQtyUser");
            builder
                .Property(b => b.ChildWoId)
                .HasColumnName("ChildWoId");
            builder
                .Property(b => b.ProcPlanId)
                .HasColumnName("ProcPlanId");
            builder
                .Property(b => b.TestData)
                .HasColumnName("TestData");
            builder
                .Property(b => b.SaNestLevel)
                .HasColumnName("SaNestLevel");
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("BOMList_TenantId");
        }
    }
}
