using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class ReferenceDocumentConfigurations : IEntityTypeConfiguration<ReferenceDocument>
    {
        public void Configure(EntityTypeBuilder<ReferenceDocument> builder)
        {
            builder
             .ToTable("ReferenceDocuments");
            builder
               .HasKey(b => b.Id);
            builder
                .HasOne(b => b.MasterPart)
                .WithMany(b => b.ReferenceDocuments)
                .HasForeignKey(b => b.MasterPartId)
                .IsRequired();
            builder
               .Property(b => b.DocumentTypeId)
               .HasColumnName("DocumentTypeId")
               .IsRequired();
            builder
               .Property(b => b.DocumentId)
               .HasColumnName("DocumentId")
               .IsRequired();
            builder
                .Property(b => b.Notes)
                .HasColumnName("Notes")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
            builder
                .Property(b => b.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(b => b.TenantId).HasDatabaseName("ReferenceDocument_TenantId");
            builder.HasIndex(b => b.MasterPartId).HasDatabaseName("ReferenceDocument_MasterPartId");
            builder.HasIndex(b => b.DocumentTypeId).HasDatabaseName("ReferenceDocument_DocumentTypeId");
        }
    }
}
