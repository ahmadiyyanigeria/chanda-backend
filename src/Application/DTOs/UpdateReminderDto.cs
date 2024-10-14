using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record UpdateReminderDto
    {
        public Guid Id { get; set; }
        public int Day { get; init; }
        public int Hour { get; init; }
        public int Minute { get; init; }
        public string ReminderTitle { get; init; } = default!;
        public string? Description { get; init; }
        public bool ViaSMS { get; init; }
        public bool ViaMail { get; init; }
    }
}
