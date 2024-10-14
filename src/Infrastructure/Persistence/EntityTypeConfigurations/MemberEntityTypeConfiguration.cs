using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("members");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(120)")
                .IsRequired();

            builder.Property(m => m.ChandaNo)
                .HasColumnName("chanda_no")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.HasIndex(m => m.ChandaNo)
                 .IsUnique();

            builder.Property(m => m.Id)
                .HasColumnName("id");

            builder.Property(m => m.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(m => m.PhoneNo)
                .HasColumnName("phone_no")
                .HasColumnType("varchar(50)");

            builder.Property(m => m.JamaatId)
            .HasColumnName("jamaat_id")
            .IsRequired();

            builder.Property(m => m.MemberLedgerId)
            .HasColumnName("member_ledger_id")
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

            builder.HasMany(m => m.MemberRoles)
                    .WithOne(mr => mr.Member)
                    .HasForeignKey(mr => mr.MemberId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.MemberLedger)
                    .WithOne(ml => ml.Member)
                    .HasForeignKey<MemberLedger>(ml => ml.MemberId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.Reminders)
                .WithOne(r => r.Member)
                .HasForeignKey(r => r.MemberId);
        }
    }
}
