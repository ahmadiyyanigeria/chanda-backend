using Application.Paging;
using Domain.Enums;
using MediatR;
using static Application.Queries.GetMemberReport;

namespace Application.Queries
{
    public class GetCircuitReport
    {
        public record Query(Guid CircuitId, string? ChandaType, bool UsePaging) : PageRequest, IRequest<CircuitReport>;

        public record CircuitReport
        {
            public decimal TotalAmountSummary { get; set; }
            public int PaidByNoOfJamaats { get; set; }
            public PaginatedList<CircuitReportResponse> ReportResponses { get; set; } = new PaginatedList<CircuitReportResponse>();
        }

        public record CircuitReportResponse
        {
            public Guid CircuitId { get; set; }
            public string CircuitName { get; set; } = default!;
            public MonthOfTheYear Month { get; set; }
            public int Year { get; set; }
            public decimal MonthAmount { get; set; }
            public int NoOfJamaats { get; set; }
            public List<Item> Items { get; set; } = new List<Item>();
        }
    }
}
