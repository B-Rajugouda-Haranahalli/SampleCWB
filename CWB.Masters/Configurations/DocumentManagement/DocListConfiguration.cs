using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.DocumentManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Configurations.DocumentManagement
{
    public class DocListConfiguration : IEntityTypeConfiguration<DocList>
    {
        public void Configure(EntityTypeBuilder<DocList> builder)
        {
            builder
            .ToTable("DocList");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.DocumentTypeId)
                .HasColumnName("DocumentTypeId")
                .IsRequired();
            builder
                .Property(t => t.FileName)
                .HasColumnName("FileName")
                .IsRequired();
            builder
                .Property(t => t.StorageLocation)
                .HasColumnName("StorageLocation")
                .IsRequired();
            builder
                .Property(c => c.UploadUiId)
                .HasColumnName("UploadUiId")
                .IsRequired();
            builder
                .Property(c => c.WoId)
                .HasColumnName("WoId")
                .IsRequired();
            builder
                .Property(c => c.SoId)
                .HasColumnName("SoId")
                .IsRequired();
            builder
                .Property(c => c.PartId)
                .HasColumnName("PartId")
                .IsRequired();
            builder
                .Property(c => c.RoutingId)
                .HasColumnName("RoutingId")
                .IsRequired();
            builder
                .Property(c => c.OprNo)
                .HasColumnName("OprNo")
                .IsRequired();
            builder
                .Property(c => c.DeletionDate)
                .HasColumnName("DeletionDate")
                .IsRequired();
            builder
                .Property(t => t.Comments)
                .HasColumnName("Comments");
            builder
                .Property(c => c.McTypeId)
                .HasColumnName("McTypeId")
                .IsRequired();
            builder
                .Property(c => c.McId)
                .HasColumnName("McId")
                .IsRequired();
            builder
                .Property(c => c.Status)
                .HasColumnName("Status")
                .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("DocList_TenantId");
        }
    }
}
