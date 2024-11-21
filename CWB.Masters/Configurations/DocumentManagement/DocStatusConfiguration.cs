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
    public class DocStatusConfiguration : IEntityTypeConfiguration<DocStatus>
    {
        public void Configure(EntityTypeBuilder<DocStatus> builder)
        {
            builder
            .ToTable("DocStatus");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.Status)
                .HasColumnName("Status")
                .IsRequired();
            builder.ConfigureBase();

            builder.HasData(
        new DocStatus { Id = 1, Status = "Active" },
        new DocStatus { Id = 2, Status = "Archieved" },
        new DocStatus { Id = 3, Status = "Deleted" }
    );
        }
    }
}
