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
    public class DocCategoryConfiguration : IEntityTypeConfiguration<DocCategory>
    {
        public void Configure(EntityTypeBuilder<DocCategory> builder)
        {
            builder
            .ToTable("DocCategory");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.DocCategoryDesc)
                .HasColumnName("DocCategoryDesc")
                .IsRequired();
            builder.ConfigureBase();

            builder.HasData(
        new DocCategory { Id = 1, DocCategoryDesc = "Reference Files" },
        new DocCategory { Id = 2, DocCategoryDesc = "Data Entry Format" }
    );
        }
    }
}
