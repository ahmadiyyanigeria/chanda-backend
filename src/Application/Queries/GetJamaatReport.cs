using Application.Paging;
using Domain.Enums;
using MediatR;
using static Application.Queries.GetMemberReport;

namespace Application.Queries
{
    public class GetJamaatReport
    {
        public record Query(Guid JamaatId, string? ChandaType, bool UsePaging) : PageRequest, IRequest<JamaatReport>;

        public record JamaatReport
        {
            public decimal TotalAmountSummary { get; set; }
            public int PaidByNoOfMembers { get; set; }
            public PaginatedList<JamaatReportResponse> ReportResponse { get; set; } = new PaginatedList<JamaatReportResponse>();
        }

        public record JamaatReportResponse
        {
            public Guid JamaatId { get; set; }
            public string JamaatName { get; set; } = default!;
            public MonthOfTheYear Month { get; set; }
            public int Year { get; set; }
            public decimal MonthAmount { get; set; }
            public int NoOfMembers { get; set; }
            public List<Item> Items { get; set; } = new List<Item>();
        }
    }
}
