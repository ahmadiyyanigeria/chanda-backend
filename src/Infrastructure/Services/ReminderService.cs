using Application.Contracts;
using Application.DTOs;
using Application.Mailing;
using Domain.Entities;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IMailProvider _email;
        private readonly ILogger<ReminderService> _logger;

        public ReminderService(IMailProvider email, ILogger<ReminderService> logger)
        {
            _email = email;
            _logger = logger;
        }

        public void ScheduleReminder(ReminderDto reminder)
        {
            RecurringJob.AddOrUpdate(reminder.Id, () => SendReminderNotification(reminder), reminder.CronExpression);
            _logger.LogInformation($"Reminder scheduled successfully for {reminder.Name}-{reminder.Email}");
        }

        public void RemoveReminder(Guid reminderId)
        {
            RecurringJob.RemoveIfExists(reminderId.ToString());
            _logger.LogInformation($"Reminder removed successfully.");
        }

        public async Task SendReminderNotification(ReminderDto reminder)
        {
            var topic = string.IsNullOrEmpty(reminder.Description) ? reminder.ReminderTitle : $"{reminder.ReminderTitle} ({reminder.Description})";
            var body = $"Asalam alaykum waramotulah wabarakatuhu,\n\nThis is to remind you base on your setting about {topic}.\nJazakumllah Khairan.";
            
            if (reminder.ViaMail)
            {
                var mailRequest = new MailRequest(reminder.Email, $"{reminder.ReminderTitle} Reminder", body, reminder.Name);

                await _email.SendAsync(mailRequest, CancellationToken.None);
                _logger.LogInformation($"Mail reminder sent to {reminder.Email} successfully at {DateTime.Now}.");
            }

            if(reminder.ViaSMS)
            {
                //Todo....
                //Send SMS...
                _logger.LogInformation($"SMS reminder sent to {reminder.PhoneNo} successfully at {DateTime.Now}.");
            }            
        }
    }
}
