﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mailing
{
    public interface IEmailService
    {
        Task Send(string from, string fromName, string to, string toName, string subject, string message, IDictionary<string, Stream>? attachments = null);

        Task SendBulk(string from, string fromName, IDictionary<string, string> tos, string subject, string message, IDictionary<string, Stream>? attachments = null);
    }
}
