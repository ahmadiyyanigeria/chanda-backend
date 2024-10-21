using Application.Paging;
using static Application.Queries.GetCircuitJamaatsReport;
using static Application.Queries.GetCircuitReport;
using static Application.Queries.GetJamaatMembersReport;
using static Application.Queries.GetJamaatReport;
using static Application.Queries.GetMemberReport;
using static Application.Queries.GetOverrallSummary;

namespace Application.Repositories
{
    public interface IInvoiceItemRepository
    {
        Task<MemberReport> GetMemberReportAsync(Guid? id, string? chandaType, PageRequest request, bool usePaging);
        Task<JamaatMembersReport> GetJamaatMembersReportAsync(Guid jamaatId, string? chandaType, PageRequest request);
        Task<JamaatReport> GetJamaatReportAsync(Guid jamaatId, string? chandaType, PageRequest request, bool usePaging);
        Task<CircuitReport> GetCircuitReportAsync(Guid circuitId, string? chandaType, PageRequest request, bool usePaging);
        Task<CircuitJamaatsReport> GetCircuitJamaatsReportAsync(Guid circuitId, string? chandaType, PageRequest request);
        Task<OverrallReport> GetOverrallReportAsync(string? chandaType, PageRequest request, bool usePaging);
    }
}
