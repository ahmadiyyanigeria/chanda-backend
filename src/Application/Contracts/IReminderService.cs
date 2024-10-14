using Application.DTOs;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IReminderService
    {
        void ScheduleReminder(ReminderDto reminder);
        void RemoveReminder(Guid reminderId);
    }
}
