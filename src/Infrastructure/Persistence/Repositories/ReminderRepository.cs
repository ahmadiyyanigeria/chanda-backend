using Application.Repositories;
using Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly AppDbContext _context;

        public ReminderRepository(AppDbContext context) 
        {  
            _context = context; 
        }

        public async Task<Reminder> AddAsync(Reminder reminder)
        {
            await _context.Reminders.AddAsync(reminder);
            return reminder;
        }

        public void DeleteReminder(Reminder reminder)
        {
            _context.Reminders.Remove(reminder);
        }

        public async Task<List<Reminder>> GetMemberReminders(Guid memberId)
        {
            return await _context.Reminders.Where(r => r.MemberId == memberId).ToListAsync();
        }

        public async Task<Reminder?> GetAsync(Guid id)
        {
            return await _context.Reminders.Include(r => r.Member).SingleOrDefaultAsync(r => r.Id == id);
        }

        public Task<Reminder> UpdateAsync(Reminder reminder)
        {
            _context.Entry(reminder).State = EntityState.Modified;
            return Task.FromResult(reminder);
        }
    }
}
