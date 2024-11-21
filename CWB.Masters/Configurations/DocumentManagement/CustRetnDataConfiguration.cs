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
    public class CustRetnDataConfiguration : IEntityTypeConfiguration<CustRetnData>
    {
        public void Configure(EntityTypeBuilder<CustRetnData> builder)
        {
            builder
            .ToTable("CustRetnData");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.DocumentTypeId)
                .HasColumnName("DocumentTypeId")
                .IsRequired();
            builder
                .Property(t => t.ComapanyId)
                .HasColumnName("ComapanyId")
                .IsRequired();
            builder
                .Property(t => t.RetPerMon)
                .HasColumnName("RetPerMon")
                .IsRequired();
            builder
                .Property(c => c.RetPerYear)
                .HasColumnName("RetPerYear")
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
            builder.HasIndex(c => c.TenantId).HasDatabaseName("CustRetnData_TenantId");
        }
    }
}
