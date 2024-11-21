using CWB.CommonUtils.Common.Configurations;
using CWB.Constants.UserIdentity;
using CWB.Masters.Domain.Routings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    /*
     *   public int RoutingStepId { get; set; }
        public int SupplierId { get; set; }
        public string WorkDone {  get; set; }
        public int TransportTime {  get; set; }
        public string CostPerPart { get; set; }
        public string Notes { get; set; }
     */
    public class SubConDetailsConfiguration
        : IEntityTypeConfiguration<SubConDetails>
    {
        public void Configure(EntityTypeBuilder<SubConDetails> builder)
        {
            builder
             .ToTable("SubConDetails");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(m => m.RoutingStepId)
                .HasColumnName("RoutingStepId")
                .HasMaxLength(300)
                .IsRequired();
            builder
                .Property(t => t.SupplierId)
                .HasColumnName("SupplierId")
                .IsRequired();
            builder
               .Property(m => m.WorkDone)
               .HasColumnName("WorkDone")
               .HasMaxLength(4000)
               .IsRequired();
            builder
               .Property(m => m.TransportTime)
               .HasColumnName("TransportTime")
               .IsRequired();
            builder
              .Property(m => m.CostPerPart)
              .HasColumnName("CostPerPart")
              .IsRequired();
            builder
              .Property(m => m.Notes)
              .HasColumnName("Notes");
            builder
            .Property(m => m.PreferredSubCon)
            .HasColumnName("PreferredSubCon");
            builder
            .Property(m => m.Deleted)
            .HasColumnName("Deleted")
            .IsRequired();
            builder
             .Property(m => m.OrigSubConId)
             .HasColumnName("OrigSubConId")
             .IsRequired();
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
        }
    }
}
