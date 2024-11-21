using CWB.CommonUtils.Common.Configurations;
using CWB.Modules.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Modules.Configurations
{
    public class ModuleTenantConfigConfigurations : IEntityTypeConfiguration<ModuleTenantConfig>
    {
        public void Configure(EntityTypeBuilder<ModuleTenantConfig> builder)
        {
            builder
             .ToTable("ModuleTenantConfigs");
            builder
               .HasKey(m => m.Id);
            builder
                .Property(m => m.ModuleId)
                .HasColumnName("ModuleId")
                .IsRequired();
            builder
               .HasOne(m => m.Module)
               .WithMany(m => m.ModuleTenantConfigs)
               .HasForeignKey(m => m.ModuleId)
               .IsRequired();

            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();

            builder.HasIndex(m => m.ModuleId).HasDatabaseName("ModuleTenantConfig_ModuleId");
        }
    }
}
