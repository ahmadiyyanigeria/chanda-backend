using Application.Exceptions;
using Application.Repositories;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetMember
    {
        public record Query(string CahandaNo) : IRequest<MemberResponse>;
        public record MemberResponse(Guid Id, string ChandaNo, string Name, string Email, string PhoneNo, string JamaatName, string CircuitName, IReadOnlyList<string> Roles);

        public class Handler : IRequestHandler<Query, MemberResponse>
        {
            private readonly IMemberRepository _memberRepository;

            public Handler(IMemberRepository memberRepository)
            {
                _memberRepository = memberRepository;
            }

            public async Task<MemberResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var member = await _memberRepository.GetMemberAsync(m => m.ChandaNo == request.CahandaNo);
                if(member == null)
                {
                    throw new NotFoundException("Member not found", ExceptionCodes.MemberNotFound.ToString(), 404);
                }

                return member.Adapt<MemberResponse>();
            }
        }
    }
}
