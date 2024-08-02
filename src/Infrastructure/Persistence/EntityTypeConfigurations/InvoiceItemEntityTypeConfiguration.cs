using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class InvoiceItemEntityTypeConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.ToTable("invoice_items");

            builder.HasKey(ii =>  ii.Id);

            builder.Property(ii => ii.PayerId)
                .HasColumnName("payer_id")
                .IsRequired();

            builder.Property(ii => ii.InvoiceId)
                .HasColumnName("invoice_id")
                .IsRequired();


            builder.Property(ii => ii.Id)
                .HasColumnName("id");

            builder.Property(ct => ct.ReceiptNo)
                   .HasColumnName("receipt_no")
                   .IsRequired();

            builder.HasIndex(m => m.ReceiptNo)
                 .IsUnique();

            builder.Property(ii => ii.MonthPaidFor)
                .HasColumnName("month_paid_for")
                .HasColumnType("varchar(50)")
                .HasConversion<EnumToStringConverter<MonthOfTheYear>>()
                .IsRequired();

            builder.Property(ii => ii.Year)
                .HasColumnName("year")
                .IsRequired();

            builder.Property(ii => ii.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(ii => ii.CreatedBy)
               .HasColumnName("created_by")
               .HasColumnType("varchar(255)");

            builder.Property(ii => ii.ModifiedBy)
               .HasColumnName("modified_by")
               .HasColumnType("varchar(255)");

            builder.Property(ii => ii.CreatedOn)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(ii  => ii.ModifiedOn)
                .HasColumnName("modified_date");

            builder.Property(ii => ii.IsDeleted)
                .HasColumnName("is_deleted")
                .IsRequired();

            builder.HasOne(ii => ii.Member)
            .WithMany() 
            .HasForeignKey(ii => ii.PayerId)
            .OnDelete(DeleteBehavior.Restrict);
                       
            builder.HasOne(ii => ii.Invoice)
                .WithMany(i => i.InvoiceItems)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ii => ii.ChandaItems)
                .WithOne(ci => ci.InvoiceItem)
                .HasForeignKey(ci => ci.InvoiceItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
