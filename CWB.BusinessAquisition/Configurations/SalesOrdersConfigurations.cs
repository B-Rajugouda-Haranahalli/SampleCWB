using CWB.BusinessAquisition.Domain;
using CWB.CommonUtils.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.BusinessAquisition.Configurations
{
    public class SalesOrdersConfigurations : IEntityTypeConfiguration<SalesOrder>
    {
        public void Configure(EntityTypeBuilder<SalesOrder> builder)
        {
            builder
            .ToTable("SalesOrders");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.CustomerOrderId)
                .HasColumnName("CustomerOrderId")
                .IsRequired();
            builder
                .Property(t => t.WorkOrderId)
                .HasColumnName("WorkOrderId")
                .IsRequired();
            /*builder
                .Property(t => t.ScheduleId)
                .HasColumnName("ScheduleId")
                .IsRequired();*/
            builder
                .Property(t => t.WorkOrderNo)
                .HasColumnName("WONumber")
                .IsRequired();
            builder
              .Property(t => t.SONumber)
              .HasColumnName("SONumber")
              .IsRequired();
            builder
             .Property(t => t.SODate)
             .HasColumnName("SODate")
             .IsRequired();
            builder
            .Property(t => t.PartId)
            .HasColumnName("PartId")
            .IsRequired();
            builder
            .Property(t => t.RequiredQuantity)
            .HasColumnName("RequiredQuantity")
            .IsRequired();
            builder
           .Property(t => t.RequiredByDate)
           .HasColumnName("RequiredByDate")
           .IsRequired();
            builder
                .Property(c => c.ActQuantity)
                .HasColumnName("ActQuantity")
                .IsRequired();
            builder
              .Property(c => c.ActCompletedDate)
              .HasColumnName("ActCompletedDate")
              .IsRequired(false);
           builder
             .Property(c => c.BalanceSOQty)
             .HasColumnName("BalanceSOQty")
             .HasDefaultValue(0);
            builder
                 .Property(c => c.Status)
                 .HasColumnName("Status");
            builder
                .Property(c => c.Plan)
                .HasColumnName("Plan");
            builder
             .Property(c => c.Changed)
             .HasColumnName("Changed")
             .HasDefaultValue(0);
            builder
             .Property(c => c.CriticalPart)
             .HasColumnName("CriticalPart")
             .HasDefaultValue(0);
            builder
                .Property(c => c.Matl)
                .HasColumnName("Matl");
            builder
                .Property(c => c.WIP)
                .HasColumnName("WIP");
            builder
               .Property(c => c.Hold)
               .HasColumnName("Hold");
            builder
              .Property(c => c.Done)
              .HasColumnName("Done");
            builder
              .Property(c => c.Comment)
              .HasColumnName("Comment");
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("SalesOrder_TenantId");
        }
    }
}
