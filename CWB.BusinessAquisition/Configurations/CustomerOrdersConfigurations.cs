using CWB.BusinessAquisition.Domain;
using CWB.CommonUtils.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.BusinessAquisition.Configurations
{
    public class CustomerOrdersConfigurations : IEntityTypeConfiguration<CustomerOrder>
    {
        public void Configure(EntityTypeBuilder<CustomerOrder> builder)
        {
            builder
            .ToTable("CustomerOrders");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.CustomerId)
                .HasColumnName("CustomerId")
                .IsRequired();
            builder
                .Property(t => t.CustomerName)
                .HasColumnName("CustomerName")
                .IsRequired();
            builder
                .Property(t => t.PONumber)
                .HasColumnName("PONumber")
                .IsRequired();
            builder
                .Property(t => t.OrderType)
                .HasColumnName("OrderType")
                .IsRequired();
            builder
                .Property(t => t.PODate)
                .HasColumnName("PODate")
                .IsRequired(); 
            builder
                .Property(t => t.DirectEntryDetails)
                .HasColumnName("DirectEntryDetails")
                .IsRequired();
            builder
               .Property(t => t.Comment)
               .HasColumnName("Comment")
               .IsRequired();
            builder
               .Property(t => t.LineNo)
               .HasColumnName("LineNo")
               .HasMaxLength(255)
               .IsRequired();
            builder
                .Property(c => c.Status)
                .HasColumnName("Status");
            builder
                .Property(c => c.Plan)
                .HasColumnName("Plan");
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
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("CustomerOrders_TenantId");
        }
    }
}
