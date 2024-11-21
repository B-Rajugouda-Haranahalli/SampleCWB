using CWB.CommonUtils.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    public class CompanyConfigurations : IEntityTypeConfiguration<Domain.Company>
    {
        public void Configure(EntityTypeBuilder<Domain.Company> builder)
        {
            builder
             .ToTable("Companies");
            builder
               .HasKey(m => m.Id);

            builder
                .Property(m => m.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(t => t.Type)
                .HasConversion<string>()
                .HasColumnName("Type")
                .IsUnicode(true)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(m => m.TenantId).HasDatabaseName("Company_TenantId");
        }
    }
}
