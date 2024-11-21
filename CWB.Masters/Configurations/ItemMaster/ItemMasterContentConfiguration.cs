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
    public class ItemMasterContentConfiguration : IEntityTypeConfiguration<ItemMasterContent>
    {
        public void Configure(EntityTypeBuilder<ItemMasterContent> builder)
        {
            builder
            .ToTable("ItemMasterContent");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.ContentDesc)
                .HasColumnName("ContentDesc")
                .IsRequired();
            builder.ConfigureBase();

            builder.HasData(
        new ItemMasterContent { Id = 1, ContentDesc = "Manf Part - Child Part" },
        new ItemMasterContent { Id = 2, ContentDesc = "Manf Part - Assembly" },
        new ItemMasterContent { Id = 3, ContentDesc = "RM - Customer Supplied" },
        new ItemMasterContent { Id = 4, ContentDesc = "RM - Purchased - Standard" },
        new ItemMasterContent { Id = 5, ContentDesc = "RM - Purchased - Made to Print" },
        new ItemMasterContent { Id = 6, ContentDesc = "BOF - Standard" },
        new ItemMasterContent { Id = 7, ContentDesc = "BOF - Made to Print" },
        new ItemMasterContent { Id = 8, ContentDesc = "BOF - Catalog" }
    );
        }
    }
}
