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
    public class LedgerEntityTypeConfiguration : IEntityTypeConfiguration<Ledger>
    {
        public void Configure(EntityTypeBuilder<Ledger> builder)
        {
            builder.ToTable("ledgers");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.ChandaTypeId)
                .HasColumnName("chanda_type_id")
                .IsRequired();

            builder.Property(l => l.MonthPaidFor)
                .HasColumnName("month_paid_for")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(l => l.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(l => l.EntryDate)
                .HasColumnName("entry_date")
                .IsRequired();

            builder.Property(l => l.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(l => l.MemberLedgerId)
                .HasColumnName("member_ledger_id");

            builder.Property(l => l.JamaatLedgerId)
                .HasColumnName("jamaat_ledger_id");

            builder.Property(l => l.Id)
                .HasColumnName("id");

            builder.Property(l => l.CreatedBy)
                .HasColumnName("created_by")
                .HasColumnType("varchar(255)");

            builder.Property(l => l.ModifiedBy)
                .HasColumnName("modified_by")
                .HasColumnType("varchar(255)");

            builder.Property(l => l.CreatedOn)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(l => l.ModifiedOn)
                .HasColumnName("modified_date");

            builder.Property(l => l.IsDeleted)
                .HasColumnName("is_deleted")
                .IsRequired();

            builder.HasOne(l => l.ChandaType)
                .WithMany()
                .HasForeignKey(l => l.ChandaTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.MemberLedger)
                .WithMany(ml => ml.LedgerList)
                .HasForeignKey(l => l.MemberLedgerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.JamaatLedger)
                .WithMany(jl => jl.LedgerList)
                .HasForeignKey(l => l.JamaatLedgerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
