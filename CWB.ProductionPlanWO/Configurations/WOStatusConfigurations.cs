using CWB.CommonUtils.Common.Configurations;
using CWB.ProductionPlanWO.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Configurations
{
    public class WOStatusConfigurations:IEntityTypeConfiguration<WOStatus>
    {
        public void Configure(EntityTypeBuilder<WOStatus> builder)
        {
            builder
                .ToTable("WOStatus");
            builder
                .HasKey(c => c.Id);
            builder
                .Property(c => c.Status)
                .HasColumnName("Status")
                .IsRequired();
            //builder.ConfigureBase();

            builder.HasData(
        new WOStatus {Id= 1, Status = "w/f Detailed Prodn Planning" },
        new WOStatus { Id = 2, Status = "w/f WO Release" },
        new WOStatus { Id = 3, Status = "w/f Input Matl" },
        new WOStatus { Id = 4, Status = "w/f Prodn" },
        new WOStatus { Id = 5, Status = "WIP" },
        new WOStatus { Id = 6, Status = "Complete" },
        new WOStatus { Id = 7, Status = "Short Closed" },
        new WOStatus { Id = 8, Status = "Hold" },
        new WOStatus { Id = 9, Status = "Deleted" }
    );

            
        }
    }
}
