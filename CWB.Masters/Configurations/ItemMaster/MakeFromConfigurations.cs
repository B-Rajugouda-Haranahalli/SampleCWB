using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations.ItemMaster
{
    public class MakeFromConfigurations : IEntityTypeConfiguration<MPMakeFrom>
    {
        public void Configure(EntityTypeBuilder<MPMakeFrom> builder)
        {
            builder
            .ToTable("MPRawMeterials");
            builder
               .HasKey(c => c.Id);
            builder
                .Property(t => t.PartId)
                .HasColumnName("PartId")
                .HasMaxLength(255);
            builder
                .Property(t => t.PartMadeFrom)
                .HasColumnName("PartMadeFrom")
                .HasMaxLength(255);
            builder
                .Property(t => t.PartDescription)
                .HasColumnName("PartDescription")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .IsRequired();
            builder
                .Property(t => t.InputWeight)
                .HasColumnName("InputWeight")
                .IsUnicode(true)
                .HasMaxLength(255);
            /**            
             *          builder
                           .Property(t => t.Description)
                           .HasConversion<string>()
                           .HasColumnName("Description")
                           .IsUnicode(true)
                           .HasMaxLength(255);*/
            builder
                .Property(t => t.ScrapGenerated)
                .HasColumnName("ScrapGenerated")
                .IsUnicode(true)
                .HasMaxLength(255);
            builder
                .Property(t => t.QuantityPerInput)
                .HasColumnName("QuantityPerInput")
                .IsUnicode(true)
                .HasMaxLength(255);
            builder
                .Property(t => t.YieldNotes)
                .HasConversion<string>()
                .HasColumnName("YieldNotes")
                .IsUnicode(true)
                .HasMaxLength(255);
            builder
                .Property(t => t.PreferedRawMaterial)
                .HasColumnName("PreferedRawMaterial")
                .HasMaxLength(255);
            builder
                .Property(c => c.ManufPartId)
                .HasColumnName("ManufPartId")
                .IsRequired();
            builder
                .Property(c => c.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(c => c.TenantId).HasDatabaseName("MPMakeFrom_TenantId");
        }
    }
}
