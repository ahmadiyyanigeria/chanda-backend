using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class ReminderEntityTypeConfiguration : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder.ToTable("reminders");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("id");

            builder.Property(r => r.MemberId)
                .HasColumnName("member_id")
                .IsRequired();

            builder.Property(r => r.Day)
                .HasColumnName("day")
                .IsRequired();

            builder.Property(r => r.Hour)
                .HasColumnName("hour")
                .IsRequired();

            builder.Property(r => r.Minute)
                .HasColumnName("minute")
                .IsRequired();

            builder.Property(r => r.IsActive)
                .HasColumnName("is_active");

            builder.Property(r => r.ViaMail)
                .HasColumnName("via_mail");

            builder.Property(r => r.ViaSMS)
                .HasColumnName("via_sms");

            builder.Property(r => r.CronExpression)
               .HasColumnName("cron_expression")
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.Property(r => r.ReminderTitle)
               .HasColumnName("reminder_title")
               .HasColumnType("varchar(255)")
               .IsRequired();

            builder.Property(r => r.Description)
               .HasColumnName("description")
               .HasColumnType("varchar(255)");
        }
    }
}
