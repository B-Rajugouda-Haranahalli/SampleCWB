using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class ItemMasterDocListConfiguration : IEntityTypeConfiguration<ItemMasterDocList>
    {
        public void Configure(EntityTypeBuilder<ItemMasterDocList> builder)
        {
            builder
            .ToTable("ItemMasterDocList");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.ContentId)
                .HasColumnName("ContentId")
                .IsRequired();
            builder
                .Property(t => t.DocumentTypeId)
                .HasColumnName("DocumentTypeId")
                .IsRequired();
            builder
                .Property(t => t.Mandatory)
                .HasColumnName("Mandatory")
                .IsRequired();
            builder
                .Property(c => c.UpdatedBy)
                .HasColumnName("UpdatedBy")
                .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("ItemMasterDocList_TenantId");
        }
    }
}
