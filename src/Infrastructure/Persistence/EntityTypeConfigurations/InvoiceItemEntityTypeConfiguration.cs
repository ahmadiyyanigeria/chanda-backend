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

            builder.Property(ii => ii.MonthPaidFor)
            .HasColumnName("month_paid_for")
            .HasColumnType("varchar(50)")
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
