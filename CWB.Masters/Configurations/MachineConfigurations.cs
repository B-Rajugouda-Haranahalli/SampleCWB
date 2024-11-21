using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class MachineConfigurations : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder
            .ToTable("Machines");
            builder
               .HasKey(c => c.Id);
            builder
                .HasOne(m => m.OperationList)
                .WithMany(m => m.Machines)
                .HasForeignKey(m => m.OperationListId)
                .IsRequired();
            builder
                .Property(c => c.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();
            builder
               .Property(c => c.Manufacturer)
               .HasColumnName("Manufacturer")
               .HasMaxLength(255)
               .IsRequired();
            builder
               .Property(c => c.SlNo)
               .HasColumnName("SlNo")
               .HasMaxLength(255)
               .IsRequired();
            builder
                .HasOne(m => m.MachineType)
                .WithMany(m => m.Machines)
                .HasForeignKey(m => m.MachineTypeId)
                .IsRequired();
            builder
               .Property(c => c.PlantId)
               .HasColumnName("PlantId")
               .IsRequired();
            builder
               .Property(c => c.ShopId)
               .HasColumnName("ShopId")
               .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("Machine_TenantId");
            builder.HasIndex(c => c.ShopId).HasDatabaseName("Machine_ShopId");
            builder.HasIndex(c => c.PlantId).HasDatabaseName("Machine_PlantId");
            builder.HasIndex(c => c.OperationListId).HasDatabaseName("Machine_OperationListId");
            builder.HasIndex(c => c.MachineTypeId).HasDatabaseName("Machine_MachineTypeId");
        }
    }
}
