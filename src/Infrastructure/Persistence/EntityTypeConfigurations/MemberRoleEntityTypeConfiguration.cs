using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class MemberRoleEntityTypeConfiguration : IEntityTypeConfiguration<MemberRole>
    {
        public void Configure(EntityTypeBuilder<MemberRole> builder)
        {
            builder.ToTable("member_roles");

            builder.HasKey(ur => ur.Id);

            builder.Property(ur => ur.Id)
                .HasColumnName("id");

            builder.Property(ur => ur.MemberId)
               .HasColumnName("user_id")
               .IsRequired();

            builder.Property(ur => ur.RoleId)
               .HasColumnName("role_id")
               .IsRequired();

            builder.Property(b => b.RoleName)
              .HasColumnName("role_name")
              .HasColumnType("varchar(255)");

            builder.Property(b => b.CreatedBy)
              .HasColumnName("created_by")
              .HasColumnType("varchar(255)");

            builder.Property(b => b.ModifiedBy)
               .HasColumnName("modified_by")
               .HasColumnType("varchar(255)");

            builder.Property(b => b.CreatedOn)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(u => u.IsDeleted)
                .HasColumnName("is_deleted");

            builder.Property(b => b.ModifiedOn)
                .HasColumnName("modified_date");
        }
    }
}
