using CWB.CommonUtils.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CWB.BusinessAquisition.Domain;

namespace CWB.BusinessAquisition.Configurations
{
    public class SOAggregateConfigurations : IEntityTypeConfiguration<SOAggregate>
    {
        public void Configure(EntityTypeBuilder<SOAggregate> builder)
        {
            builder
            .ToTable("SOAggregate");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.CustomerOrderId)
                .HasColumnName("CustomerOrderId")
                .IsRequired();
            builder
                .Property(t => t.TotalQty)
                .HasColumnName("TotalQty")
                .IsRequired();
            builder
                .Property(t => t.PartId)
                .HasColumnName("PartId")
                .IsRequired();
            builder
               .Property(t => t.Comment)
               .HasColumnName("Comment")
               .HasMaxLength(255)
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
