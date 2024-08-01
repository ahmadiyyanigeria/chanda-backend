using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class ChandaTypeEntityTypeConfiguration : IEntityTypeConfiguration<ChandaType>
    {
        public void Configure(EntityTypeBuilder<ChandaType> builder)
        {
            builder.ToTable("chanda_type");

            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Name)
                   .HasColumnName("name")
                   .IsRequired();

            builder.HasIndex(ct => ct.Name)
                 .IsUnique();

            builder.Property(ct => ct.Id)
                   .HasColumnName("id");

            builder.Property(ct => ct.Code)
                   .HasColumnName("code")
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.HasIndex(ct => ct.Code)
                   .IsUnique();

            builder.Property(ct => ct.Description)
                   .HasColumnName("description");
                  

            builder.Property(ct => ct.IncomeAccountId)
                   .HasColumnName("income_account_id")
                   .IsRequired();

            builder.Property(ct => ct.CreatedBy)
                   .HasColumnName("created_by")
                   .HasColumnType("varchar(255)");

            builder.Property(ct => ct.ModifiedBy)
               .HasColumnName("modified_by")
               .HasColumnType("varchar(255)");

            builder.Property(ct => ct.CreatedOn)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(ct => ct.ModifiedOn)
                .HasColumnName("modified_date");


        }
    }
}
