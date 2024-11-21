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
    public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder
            .ToTable("DocumentType");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.DocumentName)
                .HasColumnName("DocumentName")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(t => t.ExtnId)
                .HasColumnName("ExtnId")
                .IsRequired();
            builder
                .Property(t => t.AllowDelete)
                .HasColumnName("AllowDelete")
                .IsRequired();
            builder
                .Property(c => c.DocuCategory)
                .HasColumnName("DocuCategory")
                .IsRequired();
            builder
                .Property(c => c.DataReqdByCust)
                .HasColumnName("DataReqdByCust")
                .IsRequired();
            builder
                .Property(c => c.DefaultRetPerMon)
                .HasColumnName("DefaultRetPerMon")
                .IsRequired();
            builder
                .Property(c => c.DefaultRetPerYear)
                .HasColumnName("DefaultRetPerYear")
                .IsRequired();
            builder
                .Property(c => c.RetentionDays)
                .HasColumnName("RetentionDays")
                .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("DocumentType_TenantId");
        }
    }
}
