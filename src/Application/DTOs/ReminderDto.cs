using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record ReminderDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNo { get; set; } = default!;
        public string CronExpression { get; set; } = default!;
        public string ReminderTitle { get; set; } = default!;
        public string? Description { get; set; }
        public bool ViaSMS { get; set; }
        public bool ViaMail { get; set; }
    }
}
