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
    public class UiListConfiguration : IEntityTypeConfiguration<UiList>
    {
        public void Configure(EntityTypeBuilder<UiList> builder)
        {
            builder
            .ToTable("UiList");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.UiName)
                .HasColumnName("UiName")
                .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("UiList_TenantId");
        }
    }
}
