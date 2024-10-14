using Domain.Entities;

namespace Application.Contracts
{
    public interface IReminderService
    {
        void ScheduleReminder(Reminder reminder);
        void RemoveReminder(Guid reminderId);
    }
}
