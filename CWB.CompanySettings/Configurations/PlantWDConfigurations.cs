using CWB.CommonUtils.Common.Configurations;
using CWB.CompanySettings.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.CompanySettings.Configurations
{
    /*
     *  `PlantId` bigint NOT NULL,
  `WeeklyOff1` varchar(255) NOT NULL default "",
  `WeeklyOff2` varchar(255) NOT NULL default "",
  `NoOfShifts` int default 1,
  `FirstShiftStartTime` varchar(255) NOT NULL default "",
  `SecondShiftStartTime` varchar(255) NOT NULL default "",
  `ThirdShiftStartTime` varchar(255) NOT NULL default "",
  `FirstShiftDuration` varchar(255) NOT NULL default "",
  `SecondShiftDuration` varchar(255) NOT NULL default "",
  `ThirdShiftDuration` varchar(255) NOT NULL default "",
     * */

    public class PlantWDConfigurations : IEntityTypeConfiguration<PlantWorkingDetails>
    {
        public void Configure(EntityTypeBuilder<PlantWorkingDetails> builder)
        {

            builder
                  .ToTable("PlantWorkingDetails");
            builder.ConfigureBase();
            builder
                .Property(w => w.PlantId)
                .HasColumnName("PlantId")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder
                .Property(t => t.WeeklyOff1)
                .HasColumnName("WeeklyOff1");
            builder
                .Property(t => t.WeeklyOff2)
                .HasColumnName("WeeklyOff2");
            builder
                .Property(t => t.NoOfShifts)
                .HasColumnName("NoOfShifts");
            builder
                .Property(o => o.FirstShiftStartTime)
                .HasColumnName("FirstShiftStartTime")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(o => o.SecondShiftStartTime)
                .HasColumnName("SecondShiftStartTime")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(o => o.ThirdShiftStartTime)
                .HasColumnName("ThirdShiftStartTime")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(o => o.FirstShiftDuration)
                .HasColumnName("FirstShiftDuration")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(o => o.SecondShiftDuration)
                .HasColumnName("SecondShiftDuration")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(o => o.ThirdShiftDuration)
                .HasColumnName("ThirdShiftDuration")
                .HasMaxLength(255)
                .IsRequired();
            builder.HasIndex(m => m.TenantId).HasDatabaseName("PlantWorkingDetails_TenantId");
        }
    }
}
