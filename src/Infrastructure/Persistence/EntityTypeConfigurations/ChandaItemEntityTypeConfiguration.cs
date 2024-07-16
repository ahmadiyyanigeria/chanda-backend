using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class ChandaItemEntityTypeConfiguration : IEntityTypeConfiguration<ChandaItem>
    {
       public void Configure(EntityTypeBuilder<ChandaItem> builder)
       {
            builder.ToTable("chanda_items");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .HasColumnName("id");

            builder.Property(ci => ci.ChandaTypeId)
               .HasColumnName("chanda_type_id")
               .IsRequired();

            builder.Property(ci => ci.InvoiceItemId)
               .HasColumnName("invoice_item_id")
               .IsRequired();

            builder.Property(ci => ci.Amount)
               .HasColumnName("amount")
               .HasColumnType("decimal(18,2)")
               .IsRequired();

            builder.Property(ci => ci.CreatedBy)
                .HasColumnName("created_by")
                .HasColumnType("varchar(255)");

            builder.Property(ci => ci.ModifiedBy)
                .HasColumnName("modified_by")
                .HasColumnType("varchar(255)");

            builder.Property(ci => ci.CreatedOn)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(ci => ci.ModifiedOn)
                .HasColumnName("modified_date");

       }
        
    }
}