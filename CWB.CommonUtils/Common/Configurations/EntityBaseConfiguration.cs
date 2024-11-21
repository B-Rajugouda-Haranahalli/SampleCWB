using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CWB.CommonUtils.Common.Configurations
{
    public static class EntityBaseConfiguration
    {
        public static void ConfigureBase<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : BaseEntity
        {
            builder
                 .HasKey(b => b.Id);

            builder
                .Property(b => b.CreationDate)
                .HasColumnName("CreationDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp()");

            builder
                .Property(b => b.LastModifiedDate)
                .HasColumnName("LastModifiedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp()");

            builder
                .Property(b => b.Creator)
                .HasColumnName("Creator")
                .IsUnicode(true)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasDefaultValue(string.Empty);

            builder
                .Property(b => b.LastModifier)
                .HasColumnName("Modifier")
                .IsUnicode(true)
                .IsRequired(true)
                .HasMaxLength(450)
                .HasDefaultValue(string.Empty);

        }
    }
}
