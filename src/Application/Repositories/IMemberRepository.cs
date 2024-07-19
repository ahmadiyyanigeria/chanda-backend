using Application.Paging;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IMemberRepository
    {
        Task<Member> Create(Member member);
        Task<Member?> FindByChandaNoAsync(string chandaNo);
        Task<Member?> FindByIdAsync(Guid id);
        Task<Member> UpdateAsync(Member member);
        Member UpdateMember(Member member);
        Task<PaginatedList<Member>> GetMembers(PageRequest pageRequest, Guid? jamaatId, bool usePaging = true);
        Task<MemberLedger?> GetMemberLedger(Guid memberId);
        Task<IReadOnlyList<MemberRole>> GetMemberRoles(Guid memberId);
    }
}
