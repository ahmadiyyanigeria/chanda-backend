using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mailing
{
    public class MailSettings
    {
        public string ApiKey { get; set; } = default!;
        public string From { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
