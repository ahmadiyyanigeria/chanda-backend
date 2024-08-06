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
    public class JamaatEntityTypeConfiguration : IEntityTypeConfiguration<Jamaat>
    {
        public void Configure(EntityTypeBuilder<Jamaat> builder)
        {
            builder.ToTable("jamaats");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(120)")
                .IsRequired();

            builder.Property(m => m.CircuitId)
            .HasColumnName("circuit_id")
            .IsRequired();

            builder.Property(j => j.JamaatLedgerId)
            .HasColumnName("jamaat_ledger_id")
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

            builder.HasMany(m => m.Members)
                   .WithOne(mr => mr.Jamaat)
                   .HasForeignKey(mr => mr.JamaatId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(j => j.JamaatLedger)
                    .WithOne(jl => jl.Jamaat)
                    .HasForeignKey<JamaatLedger>(ml => ml.JamaatId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
