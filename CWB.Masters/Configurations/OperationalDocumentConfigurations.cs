using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class OperationalDocumentConfigurations : IEntityTypeConfiguration<OperationalDocument>
    {
        public void Configure(EntityTypeBuilder<OperationalDocument> builder)
        {
            builder
             .ToTable("OperationalDocuments");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(c => c.DocumentTypeId)
                .HasColumnName("DocumentTypeId")
                .IsRequired();
            builder
                .HasOne(c => c.OperationList)
                .WithMany(c => c.OperationalDocuments)
                .HasForeignKey(c => c.OperationListId)
                .IsRequired();
            builder
               .Property(c => c.IsMandatory)
               .HasColumnName("IsMandatory")
               .IsRequired()
               .HasDefaultValue(false);

            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("OperationalDocument_TenantId");
            builder.HasIndex(c => c.DocumentTypeId).HasDatabaseName("OperationalDocument_DocumentTypeId");
            builder.HasIndex(c => c.OperationListId).HasDatabaseName("OperationalDocument_OperationListId");
        }
    }
}
