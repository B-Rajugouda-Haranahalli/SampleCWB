using CWB.CommonUtils.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class OperationListConfigurations : IEntityTypeConfiguration<Domain.OperationList>
    {
        public void Configure(EntityTypeBuilder<Domain.OperationList> builder)
        {
            builder
             .ToTable("OperationLists");
            builder
               .HasKey(c => c.Id);

            builder
                .Property(c => c.Operation)
                .HasColumnName("Operation")
                .HasMaxLength(255)
                .IsRequired();
            builder
               .Property(c => c.IsMultiplePartsOfBOMUsed)
               .HasColumnName("IsMultiplePartsOfBOMUsed")
               .IsRequired()
               .HasDefaultValue(false);
            builder
               .Property(c => c.IsMultipleSubCon)
               .HasColumnName("Inhouse")
               .HasDefaultValue(0);
            builder
               .Property(c => c.Subcon)
               .HasColumnName("Subcon")
               .HasDefaultValue(0);

            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("OperationList_TenantId");
        }
    }
}
