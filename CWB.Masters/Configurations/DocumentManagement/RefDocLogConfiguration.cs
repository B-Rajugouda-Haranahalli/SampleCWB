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
    public class RefDocLogConfiguration : IEntityTypeConfiguration<RefDocLog>
    {
        public void Configure(EntityTypeBuilder<RefDocLog> builder)
        {
            builder
            .ToTable("RefDocLog");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.DocListId)
                .HasColumnName("DocListId")
                .IsRequired();
            builder
                .Property(t => t.PartId)
                .HasColumnName("PartId")
                .IsRequired();
            builder
                .Property(t => t.DocReasonId)
                .HasColumnName("DocReasonId")
                .IsRequired();
            builder
                .Property(t => t.Comments)
                .HasColumnName("Comments")
                .IsRequired();
            builder
                .Property(t => t.Action)
                .HasColumnName("Action")
                .IsRequired();
            builder
                .Property(t => t.UploadedBy)
                .HasColumnName("UploadedBy")
                .IsRequired();
            builder
                .Property(t => t.UploadedOn)
                .HasColumnName("UploadedOn")
                .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("RefDocLog_TenantId");
        }
    }
}
