using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasColumnName("id");

            builder.Property(b => b.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.HasIndex(b => b.Name)
                   .IsUnique();

            builder.Property(b => b.Description)
                .HasColumnType("varchar(255)")
                .HasColumnName("description");

            builder.Property(b => b.CreatedOn)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(b => b.ModifiedOn)
                .HasColumnName("modified_date");

            builder.HasMany(u => u.MemberRoles)
                    .WithOne(ur => ur.Role)
                    .HasForeignKey(ur => ur.RoleId);

            var date = new DateTime(2024, 01, 01, 3, 10, 28, 488, DateTimeKind.Utc);

            #region RoleSeed
            builder.HasData(
                 new Role("Admin", "Full administrative access across the system.", "Admin") { CreatedOn = date },
                 new Role("Amir", "Head of AMJN", "Admin") { CreatedOn = date },
                 new Role("Acting-Amir", "Acting Head of AMJN", "Admin") { CreatedOn = date },
                 new Role("Naib-Amir", "Naib Amir", "Admin") { CreatedOn = date },
                 new Role("Nationa-Gen-Sec", "National General Secretary.", "Admin") { CreatedOn = date },
                 new Role("National-Fin-Sec", "National Financial Secretary", "Admin") { CreatedOn = date },
                 new Role("National-Tajneed", "National Tajneed Secretary", "Admin") { CreatedOn = date },
                 new Role("CP", "Circuit President.", "Admin") { CreatedOn = date },
                 new Role("VCP", "Vice Circuit President.", "Admin") { CreatedOn = date },
                 new Role("Circuit-Fin-Sec", "Circuit Financial Secretary.", "Admin") { CreatedOn = date },
                 new Role("Jamaat-Fin-Sec", "Jamaat Financial Secretary.", "Admin") { CreatedOn = date },
                 new Role("Jamaat-President", "Jamaat President.", "Admin") { CreatedOn = date },
                 new Role("Circuit-Gen-Sec", "Circuit General Secretary.", "Admin") { CreatedOn = date },
                 new Role("Jamaat-Gen-Sec", "Jamaat General Secretary.", "Admin") { CreatedOn = date },
                 new Role("Member", "Jamaat Member.", "Admin") { CreatedOn = date }
                 );
            #endregion
        }
    }
}
