using CWB.CommonUtils.Common.Configurations;
using CWB.CompanySettings.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.CompanySettings.Configurations
{
    public class SectionConfigurations : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {

            builder
                  .ToTable("Sections");
            builder.ConfigureBase();
            builder
                .Property(w => w.Name)
                .HasColumnName("Name")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();
            builder
               .Property(m => m.ParentSectionId)
               .HasColumnName("ParentSectionId")
               .IsRequired();
            builder
               .HasOne(m => m.ShopDepartment)
               .WithMany(m => m.Sections)
               .HasForeignKey(m => m.ShopDepartmentId)
               .IsRequired();
            builder
               .Property(m => m.TenantId)
               .HasColumnName("TenantId")
               .IsRequired();
            builder.HasIndex(m => m.ShopDepartmentId).HasDatabaseName("Section_ShopDepartmentId");
            builder.HasIndex(m => m.TenantId).HasDatabaseName("Section_TenantId");
        }
    }
}
