using CWB.CommonUtils.Common.Configurations;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.Masters.Configurations
{
    /*
     *  ManufacturedPartType bigint NOT NULL,
   CompanyId bigint NOT null,
    PartId bigint not null,
   FinishedWeight bigint DEFAULT NULL,
   UOMId bigint DEFAULT NULL,
   MFBOM varchar(10) NOT NULL,
     */

    public class ManufacturedPartNoDetailConfigurations : IEntityTypeConfiguration<ManufacturedPartNoDetail>
    {
        public void Configure(EntityTypeBuilder<ManufacturedPartNoDetail> builder)
        {
            builder
             .ToTable("ManufacturedPartNoDetails");
            builder
               .HasKey(m => m.Id);

            builder
                .Property(m => m.ManufacturedPartType)
                .HasColumnName("ManufacturedPartType")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(m => m.CompanyId)
                .HasColumnName("CompanyId")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(t => t.PartId)
                .HasColumnName("PartId")
                .HasMaxLength(255)
                .IsRequired();
            
            builder
                .Property(t => t.FinishedWeight)
                .HasColumnName("FinishedWeight")
                .IsUnicode(true)
                .HasMaxLength(255);
          
            builder
                .Property(t => t.UOMId)
                .HasColumnName("UOMId")
                .HasMaxLength(255);
            /**
             * builder
                .Property(t => t.PartDescription)
                .HasConversion<string>()
                .HasColumnName("PartDescription")
                .IsUnicode(true)
                .HasMaxLength(4000);
            builder
                .Property(t => t.Status)
                .HasColumnName("Status")
                .HasMaxLength(255)
                .IsRequired();
            builder
                .Property(t => t.StatusChangeReason)
                .HasConversion<string>()
                .HasColumnName("StatusChangeReason")
                .IsUnicode(true)
                .HasMaxLength(255);
            builder
              .Property(t => t.RevNo)
              .HasConversion<string>()
              .HasColumnName("RevNo")
              .IsUnicode(true)
              .HasMaxLength(255);
            builder
                .Property(t => t.RevDate)
                .HasColumnName("RevDate")
                .IsUnicode(true)
                .HasMaxLength(255);*/
            builder
                .Property(m => m.TenantId)
                .HasColumnName("TenantId")
                .IsRequired();
            builder.ConfigureBase();
            builder.HasIndex(m => m.TenantId).HasDatabaseName("ManufacturedPartNoDetail_TenantId");
        }
    }
}
