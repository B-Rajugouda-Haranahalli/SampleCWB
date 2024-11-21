using CWB.CommonUtils.Common.Configurations;
using CWB.Simulation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Simulation.Configurations
{
    public class WorkDayMasterConfigurations : IEntityTypeConfiguration<WorkDayMaster>
    {
        public void Configure(EntityTypeBuilder<WorkDayMaster> builder)
        {
            builder
              .ToTable("WorkDayMaster");
            builder.ConfigureBase();
            builder
                .Property(w => w.WeeklyOff)
                .HasConversion<string>()
                .HasColumnName("WeeklyOff")
                .IsUnicode(true)
                .HasMaxLength(15)
                .IsRequired();
            builder
                .Property(w => w.NoOfShifts)
                .HasColumnName("NoOfShifts")
                .IsRequired()
                .HasDefaultValue(1);

            builder
                .Property(w => w.FirstShiftStartTime)
                .HasColumnName("FirstShiftStartTime")
                .IsUnicode(true)
                .IsRequired()
                .HasColumnType("TIME");

            builder
                .Property(w => w.FirstShiftDuration)
                .HasColumnName("FirstShiftDuration")
                .IsUnicode(true)
                .IsRequired()
                .HasColumnType("TIME");
            builder
                .Property(w => w.SecondShiftDuration)
                .HasColumnName("SecondShiftDuration")
                .IsUnicode(true)
                .IsRequired()
                .HasColumnType("TIME");

            builder
                .Property(w => w.ThirdShiftDuration)
                .HasColumnName("ThirdShiftDuration")
                .IsUnicode(true)
                .HasColumnType("TIME");
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();

        }
    }
}
