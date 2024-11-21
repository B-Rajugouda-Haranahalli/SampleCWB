using CWB.CommonUtils.Common.Configurations;
using CWB.CompanySettings.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.CompanySettings.Configurations
{
    public class DocumentTypeConfigurations : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {

            builder
                  .ToTable("DocumentTypes");
            builder.ConfigureBase();
            builder
                .Property(w => w.Name)
                .HasColumnName("Name")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(o => o.IsUploadedByUser)
                .HasColumnName("IsUploadedByUser")
                .IsRequired()
                .HasDefaultValue(false);
            builder
                .Property(w => w.Description)
                .HasColumnName("Description")
                .IsUnicode(true)
                .HasMaxLength(255);
            builder
                .Property(w => w.Extension)
                .HasColumnName("Extension")
                .IsUnicode(true)
                .HasMaxLength(30);
            //builder
            //   .HasOne(m => m.ShopDepartment)
            //   .WithMany(m => m.DocumentTypes)
            //   .HasForeignKey(m => m.ShopDepartmentId)
            //   .IsRequired();
            builder
               .Property(m => m.TenantId)
               .HasColumnName("TenantId")
               .IsRequired();
            //builder.HasIndex(m => m.ShopDepartmentId).HasDatabaseName("DocumentType_ShopDepartmentId");
            builder.HasIndex(m => m.TenantId).HasDatabaseName("DocumentType_TenantId");
        }
    }
}
