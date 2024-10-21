using Application.Paging;
using Domain.Enums;
using MediatR;
using static Application.Queries.GetJamaatReport;

namespace Application.Queries
{
    public class GetCircuitJamaatsReport
    {
        public record Query(Guid CircuitId, string? ChandaType, bool UsePaging) : PageRequest, IRequest<CircuitJamaatsReport>;

        public record CircuitJamaatsReport
        {
            public MonthOfTheYear Month { get; set; }
            public int Year { get; set; }
            public int NoOfJamaats { get; set; }
            public PaginatedList<JamaatReportResponse> ReportResponse { get; set; } = new PaginatedList<JamaatReportResponse>();
        }
    }
}
