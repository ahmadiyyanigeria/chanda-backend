using Application.Mailing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mailing
{
    public class MailSender : IMailProvider
    {
        private readonly MailSettings _settings;
        private readonly ILogger<MailSender> _logger;
        private readonly IEmailService _emailService;

        public MailSender(IOptions<MailSettings> settings, ILogger<MailSender> logger, IEmailService emailService)
        {
            _settings = settings.Value;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task SendAsync(MailRequest request, CancellationToken ct)
        {
            try
            {
                await _emailService.Send(_settings.From, _settings.Name, request.To, request.ReplyToName, request.Subject, request.Body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
