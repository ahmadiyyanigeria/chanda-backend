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
    public class MemberLedgerEntityConfiguration : IEntityTypeConfiguration<MemberLedger>
    {
        public void Configure(EntityTypeBuilder<MemberLedger> builder)
        {
            builder.ToTable("member_ledgers");
            
            builder.HasKey(e => e.Id);

            builder.Property(x => x.MemberId)
                .HasColumnName("member_id")
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
                .WithOne(x => x.MemberLedger)
                .HasForeignKey(x => x.MemberLedgerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
