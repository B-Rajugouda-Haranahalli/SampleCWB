using CWB.CommonUtils.Common.Configurations;
using CWB.Simulation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Simulation.Configurations
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
               .HasOne(m => m.ShopDepartment)
               .WithMany(m => m.DocumentTypes)
               .HasForeignKey(m => m.ShopDepartmentId)
               .IsRequired();
            builder
               .Property(m => m.TenantId)
               .HasColumnName("TenantId")
               .IsRequired();
            builder.HasIndex(m => m.ShopDepartmentId).HasDatabaseName("DocumentType_ShopDepartmentId");
            builder.HasIndex(m => m.TenantId).HasDatabaseName("DocumentType_TenantId");
        }
    }
}
