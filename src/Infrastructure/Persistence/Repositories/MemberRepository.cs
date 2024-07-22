﻿using Application.Paging;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace Infrastructure.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Member> Create(Member member)
        {
            await _context.Members.AddAsync(member);
            return member;
        }

        public async Task<Member?> FindByChandaNoAsync(string chandaNo)
        {
            return await _context.Members.Include(m => m.Jamaat).ThenInclude(j => j.Circuit).Include(m => m.MemberRoles)
                .Include(m => m.MemberLedger).ThenInclude(ml => ml.LedgerList)
                .SingleOrDefaultAsync(m => m.ChandaNo == chandaNo);
        }

        public async Task<Member?> FindByIdAsync(Guid id)
        {
            return await _context.Members.Include(m => m.Jamaat).ThenInclude(j => j.Circuit).Include(m => m.MemberRoles)
                .Include(m => m.MemberLedger).ThenInclude(ml => ml.LedgerList)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<MemberLedger?> GetMemberLedger(Guid memberId)
        {
            return await _context.MemberLedgers.Include(ml => ml.LedgerList).SingleOrDefaultAsync(ml => ml.MemberId == memberId);
        }

        public async Task<IReadOnlyList<MemberRole>> GetMemberRoles(Guid memberId)
        {
            return await _context.MemberRoles.Where(mr => mr.MemberId == memberId).Include(mr => mr.Role).ToListAsync();
        }

        public async Task<PaginatedList<Member>> GetMembers(PageRequest pageRequest, Guid? jamaatId, bool usePaging = true)
        {
            var query = jamaatId != null
                ? _context.Members.Include(m => m.Jamaat).ThenInclude(j => j.Circuit).Where(m => m.JamaatId == jamaatId).OrderBy(m => m.ChandaNo)
                : _context.Members.Include(m => m.Jamaat).ThenInclude(j => j.Circuit).OrderBy(m => m.Jamaat.Circuit.Name).OrderBy(m => m.Jamaat.Name).OrderBy(m => m.ChandaNo);

            if (!string.IsNullOrEmpty(pageRequest.Keyword))
            {
                query = query.Where(m => m.Name.Contains(pageRequest.Keyword, StringComparison.OrdinalIgnoreCase)).Order();
            }

            if (pageRequest.IsDescending)
            {
                query = query.OrderDescending();
            }

            var totalCount = await query.CountAsync();

            if (usePaging)
            {
                var offset = (pageRequest.Page - 1) * pageRequest.PageSize;
                var result = await query.Skip(offset).Take(pageRequest.PageSize).ToListAsync();
                return result.ToPaginatedList(totalCount, pageRequest.Page, pageRequest.PageSize);
            }
            else
            {
                var result = await query.ToListAsync();
                return result.ToPaginatedList(totalCount, 1, totalCount);
            }
        }

        public Task<Member> UpdateAsync(Member member)
        {
            _context.Entry(member).State = EntityState.Modified;
            return Task.FromResult(member);
        }

        public Member UpdateMember(Member member)
        {
            _context.Members.Update(member);
            return member;
        }
    }
}
