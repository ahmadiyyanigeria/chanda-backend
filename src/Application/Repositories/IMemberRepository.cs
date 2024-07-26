﻿using Application.Paging;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface IMemberRepository
    {
        Task<Member> Create(Member member);
        Task<Member?> GetMemberAsync(Expression<Func<Member, bool>> expression);
        bool ExistsByChandaNo(string chandaNo);
        Task<Member> UpdateAsync(Member member);
        Member UpdateMember(Member member);
        Task<PaginatedList<Member>> GetMembers(PageRequest pageRequest, Guid? jamaatId, bool usePaging = true);
        Task<MemberLedger?> GetMemberLedger(Guid memberId);
        Task<IReadOnlyList<MemberRole>> GetMemberRoles(Guid memberId);
    }
}
