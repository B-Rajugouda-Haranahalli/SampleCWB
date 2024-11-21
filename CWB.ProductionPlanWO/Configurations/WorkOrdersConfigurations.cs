using CWB.CommonUtils.Common.Configurations;
using CWB.ProductionPlanWO.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CWB.ProductionPlanWO.Configurations
{
    public class WorkOrdersConfigurations : IEntityTypeConfiguration<WorkOrders>
    {
        public void Configure(EntityTypeBuilder<WorkOrders> builder)
        {
            builder
                .ToTable("WorkOrders");
            builder
                .HasKey(w => w.Id);
            builder
               .Property(t => t.SalesOrderId)
               .HasColumnName("SalesOrderId")
               .IsRequired();
            builder
               .Property(t => t.ParentWoId)
               .HasColumnName("ParentWoId")
               .HasDefaultValue(0);
            builder
               .Property(t => t.WONumber)
               .HasColumnName("WONumber")
               .IsRequired(); 
            builder
             .Property(t => t.WODate)
             .HasColumnName("WODate")
             .IsRequired();
            builder
              .Property(t => t.PartId)
              .HasColumnName("PartId")
              .IsRequired();
            builder
              .Property(t => t.PartType)
              .HasColumnName("PartType")
              .HasDefaultValue(0);
            builder
              .Property(t => t.Parentlevel)
              .HasColumnName("Parentlevel")
              .HasDefaultValue('N');
            builder
              .Property(t => t.ManufRMLinkId)
              .HasColumnName("ManufRMLinkId")
              .IsRequired();
            builder
             .Property(t => t.BuildToStock)
             .HasColumnName("BuildToStock")
             .HasDefaultValue(0);
            builder
             .Property(t => t.TestData)
             .HasColumnName("TestData")
             .HasDefaultValue(0);
            builder
             .Property(t => t.CalcWOQty)
             .HasColumnName("CalcWOQty")
             .HasDefaultValue(0);
            builder
             .Property(t => t.QtyOnHand)
             .HasColumnName("QtyOnHand")
             .HasDefaultValue(0);
            builder
             .Property(t => t.AddnOtyUser)
             .HasColumnName("AddnOtyUser")
             .HasDefaultValue(0);
            builder
             .Property(t => t.Status)
             .HasColumnName("Status")
             .IsRequired();
            builder
             .Property(t => t.PlanWOQnty)
             .HasColumnName("PlanWOQnty")
             .HasDefaultValue(0);
            builder
             .Property(t => t.PlanCompletionDate)
             .HasColumnName("PlanCompletionDate")
             .HasDefaultValue(null);
            builder
             .Property(t => t.RoutingId)
             .HasColumnName("RoutingId");
            //.IsRequired();
            builder
             .Property(t => t.StartingOpNo)
             .HasColumnName("StartingOpNo");
            //.IsRequired();
            builder
             .Property(t => t.EndingOpNo)
             .HasColumnName("EndingOpNo");
            builder
             .Property(t => t.ReloadOption)
             .HasColumnName("ReloadOption");
            builder
             .Property(t => t.PPStatus)
             .HasColumnName("PPStatus");
            //.IsRequired();
            builder
             .Property(t => t.Active)
             .HasColumnName("Active")
             .HasDefaultValue(0);
            builder
             .Property(t => t.CriticalPart)
             .HasColumnName("CriticalPart")
             .HasDefaultValue(0);
            builder
             .Property(t => t.Urgent)
             .HasColumnName("Urgent")
             .HasDefaultValue(0);
            builder
             .Property(t => t.For_Ref)
             .HasColumnName("For_Ref")
             .HasDefaultValue(0);
            builder
             .Property(t => t.ManufDaysAvailable)
             .HasColumnName("ManufDaysAvailable");
             //.IsRequired();
            builder
             .Property(t => t.ManufDaysRequired)
             .HasColumnName("ManufDaysRequired");
             //.IsRequired();
            builder
             .Property(t => t.Changed)
             .HasColumnName("Changed")
             .HasDefaultValue(0);
            builder
             .Property(t => t.PlanStartDate)
             .HasColumnName("PlanStartDate")
             .HasDefaultValue(null);
            builder
             .Property(t => t.ActStartDate)
             .HasColumnName("ActStartDate")
             .HasDefaultValue(null);
            builder
             .Property(t => t.ActCompletionDate)
             .HasColumnName("ActCompletionDate")
             .HasDefaultValue(null);
            builder
             .Property(t => t.ActWOQty)
             .HasColumnName("ActWOQty");
             //.IsRequired();
            builder
               .Property(c => c.Matl)
               .HasColumnName("Matl")
             .HasDefaultValue(0);
            builder
                .Property(c => c.WIP)
                .HasColumnName("WIP")
             .HasDefaultValue(0);
            builder
               .Property(c => c.Hold)
               .HasColumnName("Hold")
             .HasDefaultValue(0);
            builder
              .Property(c => c.Done)
              .HasColumnName("Done")
             .HasDefaultValue(0);
            builder
              .Property(c => c.Comment)
              .HasColumnName("Comment")
             .HasDefaultValue("");
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("WorkOrders_TenantId");
        }
    }
}
