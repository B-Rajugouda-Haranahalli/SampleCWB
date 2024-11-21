using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class MachineProcessDocumentConfigurations : IEntityTypeConfiguration<MachineProcessDocument>
    {
        public void Configure(EntityTypeBuilder<MachineProcessDocument> builder)
        {
            builder
             .ToTable("MachineProcessDocuments");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(c => c.DocumentTypeId)
                .HasColumnName("DocumentTypeId")
                .IsRequired();
            builder
                .HasOne(c => c.Machine)
                .WithMany(c => c.MachineProcessDocuments)
                .HasForeignKey(c => c.MachineId)
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
            builder.HasIndex(c => c.TenantId).HasDatabaseName("MachineProcessDocument_TenantId");
            builder.HasIndex(c => c.DocumentTypeId).HasDatabaseName("MachineProcessDocument_DocumentTypeId");
            builder.HasIndex(c => c.MachineId).HasDatabaseName("MachineProcessDocument_MachineId");
        }
    }
}
