using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class JamaatLedgerEntityTypeConfiguration : IEntityTypeConfiguration<JamaatLedger>
    {
        public void Configure(EntityTypeBuilder<JamaatLedger> builder)
        {
            builder.ToTable("jamaat_ledgers");
            
            builder.HasKey(t => t.Id);

            builder.Property(t => t.JamaatId)
                .HasColumnName("jamaat_id")
                .IsRequired();

            builder.Property(m => m.CreatedBy)
               .HasColumnName("created_by")
               .HasColumnType("varchar(255)");

            builder.Property(m => m.ModifiedBy)
               .HasColumnName("modified_by")
               .HasColumnType("varchar(255)");

            builder.Property(m => m.CreatedOn)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(m => m.ModifiedOn)
                .HasColumnName("modified_date");

            builder.HasMany(x => x.LedgerList)
                .WithOne(x => x.JamaatLedger)
                .HasForeignKey(x => x.JamaatLedgerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
