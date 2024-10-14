namespace Domain.Entities
{
    public class Reminder: BaseEntity
    {
        public Guid MemberId { get; private set; }
        public Member Member { get; private set; } = default!;
        public int Day { get; private set; }
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public bool IsActive { get; private set; } = true;
        public string CronExpression { get; private set; }
        public string ReminderTitle { get; private set; } = default!;
        public string? Description { get; private set; }
        public bool ViaSMS { get; private set; }
        public bool ViaMail { get; private set; }


        public Reminder(Guid memberId, int day, int hour, int minute, string reminderTitle, string? description, bool viaSMS, bool viaMail)
        {
            MemberId = memberId;
            Day = day;
            Hour = hour;
            Minute = minute;
            ReminderTitle = reminderTitle;
            Description = description;
            CronExpression = GenerateCronExpression(day, minute, hour);
            ViaSMS = viaSMS;
            ViaMail = viaMail;
        }

        public void SetOwner(Member member)
        {
            Member = member;
        }

        public void UpdateStatus(bool status)
        {
            IsActive = status;
        }

        public void UpdateReminder(int day, int hour, int minute, string reminderTitle, string? description, bool viaSMS, bool viaMail)
        {
            Day = day;
            Hour = hour;
            Minute = minute;
            ReminderTitle = reminderTitle;
            Description = description;
            CronExpression = GenerateCronExpression(day, minute, hour);
            ViaSMS = viaSMS;
            ViaMail = viaMail;
            IsActive = true;
        }

        private string GenerateCronExpression(int day, int minute, int hour)
        {
            return day > 28 ? $"0 {minute} {hour} L * *" : $"0 {minute} {hour} {day} * *";
        }
    }
}
