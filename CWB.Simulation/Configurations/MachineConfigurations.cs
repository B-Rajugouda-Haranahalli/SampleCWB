using CWB.CommonUtils.Common.Configurations;
using CWB.Simulation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Simulation.Configurations
{
    public class MachineConfigurations : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder
             .ToTable("Machines");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(m => m.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(m => m.Manufacturer)
                .HasColumnName("Manufacturer")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(m => m.SerialNumber)
                .HasColumnName("SerialNumber")
                .HasMaxLength(255);
            builder
               .HasOne(m => m.Plant)
               .WithMany(m => m.Machines)
               .HasForeignKey(m => m.PlantId)
               .IsRequired();
            builder
               .HasOne(m => m.ShopDepartment)
               .WithMany(m => m.Machines)
               .HasForeignKey(m => m.ShopDepartmentId)
               .IsRequired();
            builder
               .HasOne(m => m.MachineType)
               .WithMany(m => m.Machines)
               .HasForeignKey(m => m.MachineTypeId)
               .IsRequired();
            builder
               .Property(m => m.TenantId)
               .HasColumnName("TenantId")
               .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(m => m.PlantId).HasDatabaseName("Machine_PlantId");
            builder.HasIndex(m => m.MachineTypeId).HasDatabaseName("Machine_MachineTypeId");
            builder.HasIndex(m => m.ShopDepartmentId).HasDatabaseName("Machine_ShopDepartmentId");
            builder.HasIndex(m => m.TenantId).HasDatabaseName("Machine_TenantId");
        }
    }
}
