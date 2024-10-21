using Application.Paging;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface IMemberRepository
    {
        Member? GetMember(Guid? id);
        Task<Member> CreateAsync(Member member);
        Task<Member?> GetMemberAsync(Expression<Func<Member, bool>> expression);
        bool ExistsByChandaNo(string chandaNo);
        Task<Member> UpdateAsync(Member member);
        Member UpdateMember(Member member);
        Task<PaginatedList<Member>> GetMembers(PageRequest pageRequest, Guid? jamaatId, bool usePaging = true);
        Task<MemberLedger?> GetMemberLedger(Guid memberId);
        Task<MemberLedger?> AddMemberLedgerAsync(MemberLedger memberLedger);
        Task<IReadOnlyList<MemberRole>> GetMemberRoles(Guid memberId);
        List<Member> GetMembers(Expression<Func<Member, bool>> expression);
    }
}
