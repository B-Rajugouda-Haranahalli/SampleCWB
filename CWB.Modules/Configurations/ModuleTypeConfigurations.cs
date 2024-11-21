using CWB.CommonUtils.Common.Configurations;
using CWB.Modules.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Modules.Configurations
{
    public class ModuleTypeConfigurations : IEntityTypeConfiguration<ModuleType>
    {
        public void Configure(EntityTypeBuilder<ModuleType> builder)
        {
            builder
             .ToTable("ModuleTypes");
            builder
               .HasKey(m => m.Id);

            builder
                .Property(m => m.Type)
                .HasColumnName("Type")
                .HasMaxLength(150)
                .IsRequired();
            builder.ConfigureBase();
        }
    }
}