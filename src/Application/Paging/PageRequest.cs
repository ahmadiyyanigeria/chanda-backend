using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Paging
{
    public record PageRequest
    {
        public int PageSize { get; init; } = 20;
        public int Page { get; init; } = 1;
        public string? SortBy { get; init; }
        public string? Filter { get; init; }
        public string? Keyword { get; init; }
        public bool IsDescending { get; init; } = false;
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public int Year { get; init; } = 0;
        public int Month { get; init; } = 0;
    }
}
