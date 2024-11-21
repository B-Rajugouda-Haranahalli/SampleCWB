using CWB.CommonUtils.Common.Configurations;
using CWB.Simulation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Simulation.Configurations
{
    public class ItemMasterConfigurations : IEntityTypeConfiguration<ItemMaster>
    {
        public void Configure(EntityTypeBuilder<ItemMaster> builder)
        {
            builder
             .ToTable("ItemMasters");
            builder
               .HasKey(m => m.Id);
            builder
               .HasOne(m => m.Customer)
               .WithMany(m => m.ItemMasters)
               .HasForeignKey(m => m.CustomerId)
               .IsRequired();
            builder
                .Property(m => m.PartNo)
                .HasColumnName("PartNo")
                .HasMaxLength(150)
                .IsRequired();
            builder
                .Property(m => m.RevNo)
                .HasColumnName("RevNo")
                .HasMaxLength(150)
                .IsRequired();

            builder
                .Property(m => m.RevDate)
                .HasColumnName("RevDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(t => t.PartDescription)
                .HasColumnName("PartDescription")
                .IsUnicode(true)
                .HasMaxLength(4000)
                .HasColumnType("nvarchar(MAX)");

            builder
                .Property(t => t.PartAssembly)
                .HasConversion<string>()
                .HasColumnName("PartAssembly")
                .IsUnicode(true)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(t => t.MakeBOF)
                .HasConversion<string>()
                .HasColumnName("MakeBOF")
                .IsUnicode(true)
                .HasMaxLength(15)
                .IsRequired();
            builder
                .Property(o => o.IsActive)
                .HasColumnName("IsActive")
                .IsRequired()
                .HasDefaultValue(true);

            builder.ConfigureBase();
            builder.HasIndex(m => m.CustomerId).HasDatabaseName("ItemMaster_CustomerId");
            builder.HasIndex(m => m.TenantId).HasDatabaseName("ItemMaster_TenantId");
        }
    }
}
