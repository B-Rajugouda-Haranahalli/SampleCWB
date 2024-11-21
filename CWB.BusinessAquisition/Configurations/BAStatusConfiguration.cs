using CWB.BusinessAquisition.Domain;
using CWB.CommonUtils.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.BusinessAquisition.Configurations
{
    public class BAStatusConfiguration : IEntityTypeConfiguration<BAStatus>
    {
        public void Configure(EntityTypeBuilder<BAStatus> builder)
        {
            builder
                .ToTable("BAStatus");
            builder
                .HasKey(c => c.Id);
            builder
                .Property(c => c.Status)
                .HasColumnName("Status")
                .IsRequired();
            //builder.ConfigureBase();

            builder.HasData(
        new BAStatus { Id = 1,  Status = "SO created" },
        new BAStatus { Id = 2,  Status = "Partially Planned" },
        new BAStatus { Id = 3,  Status = "Fully Planned" },
        new BAStatus { Id = 4,  Status = "WIP" },
        new BAStatus { Id = 5,  Status = "Complete" },
        new BAStatus { Id = 6,  Status = "Hold" },
        new BAStatus { Id = 7,  Status = "Short Closed" },
        new BAStatus { Id = 8,  Status = "Deleted" }
    );


        }
    }
}
