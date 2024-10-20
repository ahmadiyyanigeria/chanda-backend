using Application.Paging;
using Domain.Enums;
using MediatR;
using static Application.Queries.GetMemberReport;

namespace Application.Queries
{
    public class GetJamaatMembersReport
    {
        public record Query(Guid JamaatId, string? ChandaType, bool UsePaging) : PageRequest, IRequest<JamaatMembersReport>;

        public record JamaatMembersReport
        {
            public MonthOfTheYear Month { get; set; }
            public int Year { get; set; }
            public int NoOfMembers { get; set; }
            public PaginatedList<MemberReportResponse> ReportResponses { get; set; } = new PaginatedList<MemberReportResponse>();
        }
    }
}
