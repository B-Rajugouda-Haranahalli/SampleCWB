using CWB.CommonUtils.Common.Configurations;
using CWB.CompanySettings.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.CompanySettings.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {

            builder
                  .ToTable("City");
            builder.ConfigureBase();
            builder
                .Property(w => w.Name)
                .HasColumnName("Name")
                .IsUnicode(true)
                .HasMaxLength(255)
                .IsRequired();
            builder
               .Property(m => m.TenantId)
               .HasColumnName("TenantId")
               .IsRequired();
            builder.HasData(
            new City { Id = 1, Name = "Bangalore", TenantId = 1 },
            new City { Id = 2, Name = "New Delhi", TenantId = 1 },
            new City { Id = 3, Name = "Mumbai", TenantId = 1 },
            new City { Id = 4, Name = "Chennai", TenantId = 1 },
            new City { Id = 5, Name = "Kolkata", TenantId = 1 },
            new City { Id = 6, Name = "Hyderabad", TenantId = 1 },
            new City { Id = 7, Name = "Pune", TenantId = 1 },
            new City { Id = 8, Name = "Ahmedabad", TenantId = 1 },
            new City { Id = 9, Name = "Jaipur", TenantId = 1 },
            new City { Id = 10, Name = "Surat", TenantId = 1 },
            new City { Id = 11, Name = "Lucknow", TenantId = 1 },
            new City { Id = 12, Name = "Kanpur", TenantId = 1 },
            new City { Id = 13, Name = "Nagpur", TenantId = 1 },
            new City { Id = 14, Name = "Visakhapatnam", TenantId = 1 },
            new City { Id = 15, Name = "Bhopal", TenantId = 1 },
            new City { Id = 16, Name = "Patna", TenantId = 1 },
            new City { Id = 17, Name = "Vadodara", TenantId = 1 },
            new City { Id = 18, Name = "Indore", TenantId = 1 },
            new City { Id = 19, Name = "Coimbatore", TenantId = 1 },
            new City { Id = 20, Name = "Mysore", TenantId = 1 },
            new City { Id = 21, Name = "Agra", TenantId = 1 },
            new City { Id = 22, Name = "Nashik", TenantId = 1 },
            new City { Id = 23, Name = "Faridabad", TenantId = 1 },
            new City { Id = 24, Name = "Ludhiana", TenantId = 1 },
            new City { Id = 25, Name = "Rajkot", TenantId = 1 },
            new City { Id = 26, Name = "Kalyan-Dombivli", TenantId = 1 },
            new City { Id = 27, Name = "Vasai-Virar", TenantId = 1 },
            new City { Id = 28, Name = "Varanasi", TenantId = 1 },
            new City { Id = 29, Name = "Srinagar", TenantId = 1 },
            new City { Id = 30, Name = "Amritsar", TenantId = 1 },
            new City { Id = 31, Name = "Allahabad", TenantId = 1 },
            new City { Id = 32, Name = "Thane", TenantId = 1 },
            new City { Id = 33, Name = "Bhubaneswar", TenantId = 1 },
            new City { Id = 34, Name = "Dehradun", TenantId = 1 },
            new City { Id = 35, Name = "Guwahati", TenantId = 1 },
            new City { Id = 36, Name = "Mangalore", TenantId = 1 },
            new City { Id = 37, Name = "Ranchi", TenantId = 1 },
            new City { Id = 38, Name = "Jodhpur", TenantId = 1 },
            new City { Id = 39, Name = "Dhanbad", TenantId = 1 },
            new City { Id = 40, Name = "Kota", TenantId = 1 },
            new City { Id = 41, Name = "Gwalior", TenantId = 1 }
    );

            builder.HasIndex(m => m.TenantId).HasDatabaseName("City_TenantId");
        }
    }
}
