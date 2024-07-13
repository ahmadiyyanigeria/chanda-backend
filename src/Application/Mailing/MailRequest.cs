using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mailing
{
    public class MailRequest
    {
        public MailRequest(string to, string subject, string body, string replyToName, string? replyTo = null, List<string>? bcc = null, List<string>? cc = null, IDictionary<string, byte[]>? attachmentData = null, IDictionary<string, string>? headers = null)
        {
            To = to;
            Subject = subject;
            Body = body;
            ReplyTo = replyTo;
            ReplyToName = replyToName;
            Bcc = bcc ?? new List<string>();
            Cc = cc ?? new List<string>();
            AttachmentData = attachmentData ?? new Dictionary<string, byte[]>();
            Headers = headers ?? new Dictionary<string, string>();
        }

        public string To { get; }

        public string Subject { get; }

        public string Body { get; }

        public string? ReplyTo { get; }

        public string ReplyToName { get; }

        public List<string> Bcc { get; }

        public List<string> Cc { get; }

        public IDictionary<string, byte[]> AttachmentData { get; }

        public IDictionary<string, string> Headers { get; }
    }
}
