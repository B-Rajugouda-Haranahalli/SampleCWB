using CWB.CommonUtils.Common.Configurations;
using CWB.Modules.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Modules.Configurations
{
    public class ModuleConfigurations : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder
             .ToTable("Modules");
            builder
               .HasKey(m => m.Id);

            builder
                .Property(m => m.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();
            builder
               .Property(m => m.ModuleKey)
               .HasColumnName("ModuleKey")
               .HasMaxLength(50)
               .IsRequired();
            builder
               .HasOne(m => m.ModuleType)
               .WithMany(m => m.Modules)
               .HasForeignKey(m => m.ModuleTypeId)
               .IsRequired();
            builder
                .Property(m => m.Description)
                .HasColumnName("Description")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");
            builder
                .Property(m => m.IsActive)
                .HasColumnName("IsActive")
                .IsRequired()
                .HasDefaultValue(false);
            builder.ConfigureBase();

            builder.HasIndex(m => m.ModuleTypeId).HasDatabaseName("Module_ModuleTypeId");
        }
    }
}
