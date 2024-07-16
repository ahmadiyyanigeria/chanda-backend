using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class CircuitEntityTypeConfiguration : IEntityTypeConfiguration<Circuit>
    {
        public void Configure(EntityTypeBuilder<Circuit> builder)
        {
            builder.ToTable("circuits");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(120)")
                .IsRequired();

            builder.HasIndex(ct => ct.Name)
                   .IsUnique();

            builder.Property(c => c.CreatedBy)
               .HasColumnName("created_by")
               .HasColumnType("varchar(255)");

            builder.Property(c => c.ModifiedBy)
               .HasColumnName("modified_by")
               .HasColumnType("varchar(255)");

            builder.Property(c => c.CreatedOn)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(c => c.ModifiedOn)
                .HasColumnName("modified_date");

            builder.HasMany(c => c.Jamaats)
                    .WithOne(ci => ci.Circuit)
                    .HasForeignKey(ci => ci.CircuitId).OnDelete(DeleteBehavior.Restrict);
        
        }
    }
}