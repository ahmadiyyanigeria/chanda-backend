using Application.Paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Queries.GetCircuitReport;

namespace Application.Queries
{
    public class GetOverrallSummary
    {
        public record Query(string? ChandaType, bool UsePaging): PageRequest, IRequest<OverrallReport>;

        public record OverrallReport
        {
            public decimal TotalAmountSummary { get; set; }
            public int PaidByNoOfCircuits { get; set; }
            public PaginatedList<CircuitReportResponse> ReportResponses { get; set; } = new PaginatedList<CircuitReportResponse>();
        }
    }
}
