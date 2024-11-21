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
    public class ExtnInfoConfiguration : IEntityTypeConfiguration<ExtnInfo>
    {
        public void Configure(EntityTypeBuilder<ExtnInfo> builder)
        {
            builder
              .ToTable("ExtnInfo");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.ExtnName)
                .HasColumnName("ExtnName")
                .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("ExtnInfo_TenantId");
        }
    }
}
