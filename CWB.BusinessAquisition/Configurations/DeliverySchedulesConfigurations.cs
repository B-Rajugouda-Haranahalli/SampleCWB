using CWB.BusinessAquisition.Domain;
using CWB.CommonUtils.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.BusinessAquisition.Configurations
{
    public class DeliverySchedulesConfigurations : IEntityTypeConfiguration<DeliverySchedule>
    {
        public void Configure(EntityTypeBuilder<DeliverySchedule> builder)
        {
            builder
            .ToTable("DeliverySchedules");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.CustomerOrderId)
                .HasColumnName("CustomerOrderId")
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
                .Property(t => t.Comment)
                .HasColumnName("Comment")
                .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("DeliverySchedules_TenantId");
        }
    }
}
