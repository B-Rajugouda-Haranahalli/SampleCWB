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
    public class RefDocReasonListConfiguration : IEntityTypeConfiguration<RefDocReasonList>
    {
        public void Configure(EntityTypeBuilder<RefDocReasonList> builder)
        {
            builder
            .ToTable("RefDocReasonList");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.DocReason)
                .HasColumnName("DocReason")
                .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("RefDocReasonList_TenantId");
        }
    }
}
