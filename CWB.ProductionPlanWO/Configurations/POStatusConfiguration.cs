using CWB.ProductionPlanWO.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Configurations
{
    public class POStatusConfiguration : IEntityTypeConfiguration<POStatus>
    {
        public void Configure(EntityTypeBuilder<POStatus> builder)
        {
            builder
                .ToTable("POStatus");
            builder
                .HasKey(c => c.Id);
            builder
                .Property(c => c.Status)
                .HasColumnName("Status")
                .IsRequired();
            //builder.ConfigureBase();

            builder.HasData(
        new POStatus { Id = 1, Status = "W/f PO Release" },
        new POStatus { Id = 2, Status = "PO Sent" },
        new POStatus { Id = 3, Status = "Partial Recd" },
        new POStatus { Id = 4, Status = "Complete" },
        new POStatus { Id = 5, Status = "Short Closed" },
        new POStatus { Id = 6, Status = "Deleted" }
    );


        }
    }
}
