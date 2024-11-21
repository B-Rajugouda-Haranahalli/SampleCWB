using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWB.Masters.Domain;
using CWB.CommonUtils.Common.Configurations;

namespace CWB.Masters.Configurations
{
    public class McTypeDocListConfiguration : IEntityTypeConfiguration<McTypeDocList>
    {
        public void Configure(EntityTypeBuilder<McTypeDocList> builder)
        {
            builder
           .ToTable("McTypeDocList");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(c => c.McTypeId)
                .HasColumnName("McTypeId")
                .IsRequired();
            builder
                .Property(c => c.DocumentTypeId)
                .HasColumnName("DocumentTypeId")
                .IsRequired();
            builder
                .Property(c => c.Mandatory)
                .HasColumnName("Mandatory")
                .HasDefaultValue('N');
            builder
                .Property(c => c.UpdatedBy)
                .HasColumnName("UpdatedBy");
            builder
                .Property(c => c.UpdatedOn)
                .HasColumnName("UpdatedOn")
                .HasDefaultValue(DateTime.Now)
                .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("McTypeDocList_TenantId");
        }
    }
}
