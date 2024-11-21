using CWB.CommonUtils.Common.Configurations;
using CWB.Simulation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Simulation.Configurations
{
    public class MRBomConfigurations : IEntityTypeConfiguration<MRBom>
    {
        public void Configure(EntityTypeBuilder<MRBom> builder)
        {
            builder
                .ToTable("MRBoms");
            builder.ConfigureBase();

            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder
                .Property(t => t.ItemType)
                .HasConversion<string>()
                .HasColumnName("ItemType")
                .IsUnicode(true)
                .HasMaxLength(50)
                .IsRequired();
            builder
            .Property(t => t.ItemDescription)
            .HasColumnName("ItemDescription")
            .IsUnicode(true)
            .HasMaxLength(4000)
            .HasColumnType("nvarchar(MAX)");
            builder
               .HasOne(m => m.MRBomGroup)
               .WithMany(m => m.MRBoms)
               .HasForeignKey(m => m.MRBomGroupId)
               .IsRequired();
            builder
               .HasOne(m => m.Supplier)
               .WithMany(m => m.MRBoms)
               .HasForeignKey(m => m.SupplierId)
               .IsRequired();
            builder
                .Property(t => t.UoM)
                .HasConversion<string>()
                .HasColumnName("UoM")
                .IsUnicode(true)
                .HasMaxLength(50)
                .IsRequired();
            builder
                .Property(t => t.ConsumptionType)
                .HasConversion<string>()
                .HasColumnName("ConsumptionType")
                .IsUnicode(true)
                .HasMaxLength(50)
                .IsRequired();
            builder
                .Property(m => m.Cost)
                .HasColumnName("Cost")
                .IsRequired();
            builder
                .Property(m => m.QuantityOnHand)
                .HasColumnName("QuantityOnHand")
                .IsRequired();

            builder.HasIndex(m => m.MRBomGroupId).HasDatabaseName("MRBoms_MRBomGroupId");
            builder.HasIndex(m => m.SupplierId).HasDatabaseName("MRBoms_SupplierId");
            builder.HasIndex(m => m.TenantId).HasDatabaseName("MRBoms_TenantId");
        }
    }
}
