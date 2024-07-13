using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mailing
{
    public interface IMailProvider
    {
        Task SendAsync(MailRequest request, CancellationToken ct);
    }
}
