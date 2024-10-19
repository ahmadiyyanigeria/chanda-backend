using Application.Paging;
using static Application.Queries.GetMemberReport;

namespace Application.Repositories
{
    public interface IInvoiceItemRepository
    {
        Task<MemberReport> GetMemberReportAsync(Guid? id, string? chandaType, PageRequest request, bool usePaging);
    }
}
