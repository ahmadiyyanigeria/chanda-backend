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
    public class InvoiceEntityTypeConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoices");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .IsRequired();

           
            builder.Property(i => i.Id)
                .HasColumnName("id");

            builder.Property(i => i.JamaatId)
            .HasColumnName("jamaat_id")
            .IsRequired();
                       
            builder.Property(i => i.CreatedBy)
               .HasColumnName("created_by")
               .HasColumnType("varchar(255)");

            builder.Property(i => i.ModifiedBy)
               .HasColumnName("modified_by")
               .HasColumnType("varchar(255)");

            builder.Property(i => i.CreatedOn)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(i => i.ModifiedOn)
                .HasColumnName("modified_date");

            builder.Property(i => i.IsDeleted)
           .HasColumnName("is_deleted")
           .IsRequired();

           
            builder.HasMany(i => i.InvoiceItems)
                .WithOne(ii => ii.Invoice)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Payments)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
