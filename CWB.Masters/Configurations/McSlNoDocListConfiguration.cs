using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Configurations
{
    public class McSlNoDocListConfiguration : IEntityTypeConfiguration<McSlNoDocList>
    {
        public void Configure(EntityTypeBuilder<McSlNoDocList> builder)
        {
            builder
           .ToTable("McSlNoDocList");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(c => c.McId)
                .HasColumnName("McId")
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
            builder.HasIndex(c => c.TenantId).HasDatabaseName("McSlNoDocList_TenantId");
        }
    }
}
