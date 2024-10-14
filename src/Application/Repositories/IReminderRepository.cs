using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IReminderRepository
    {
        Task<Reminder?> GetAsync(Guid id);
        Task<List<Reminder>> GetMemberReminders(Guid memberId);
        Task<Reminder> AddAsync(Reminder reminder);
        Task<Reminder> UpdateAsync(Reminder reminder);
        void DeleteReminder(Reminder reminder);
    }
}
