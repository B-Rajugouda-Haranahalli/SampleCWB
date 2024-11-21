using CWB.CommonUtils.Common.Configurations;
using CWB.CompanySettings.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.CompanySettings.Configurations
{
    public class ShopDepartmentConfigurations : IEntityTypeConfiguration<ShopDepartment>
    {
        public void Configure(EntityTypeBuilder<ShopDepartment> builder)
        {

            builder
                  .ToTable("ShopDepartments");
            builder.ConfigureBase();
            builder
                .Property(w => w.Name)
                .HasColumnName("Name")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(w => w.NoOfShifts)
                .HasColumnName("NoOfShifts")
                .IsRequired()
                .HasDefaultValue(1);
            builder
               .HasOne(m => m.Plant)
               .WithMany(m => m.ShopDepartments)
               .HasForeignKey(m => m.PlantId)
               .IsRequired();
            builder
               .Property(m => m.TenantId)
               .HasColumnName("TenantId")
               .IsRequired();
            builder
                .Property(t => t.Activity)
                .HasColumnName("Activity")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
            builder
               .Property(t => t.ProdDept)
               .HasColumnName("ProdDept")
               .HasMaxLength(1);
            builder
              .Property(t => t.Section)
              .HasColumnName("Section")
              .HasMaxLength(255);
            builder.HasIndex(m => m.PlantId).HasDatabaseName("ShopDepartment_PlantId");
            builder.HasIndex(m => m.TenantId).HasDatabaseName("ShopDepartment_TenantId");
        }
    }
}
