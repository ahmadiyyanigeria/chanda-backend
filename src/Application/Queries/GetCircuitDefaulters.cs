using Application.Exceptions;
using Application.Paging;
using Application.Repositories;
using Domain.Constants;
using Domain.Enums;
using Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Queries
{
    public class GetCircuitDefaulters
    {
        public record Query(Guid CircuitId, string ChandaType, bool UsePaging): PageRequest, IRequest<CircuitDefaulters>;

        public record CircuitDefaulters
        {
            public Guid CircuitId { get; set; }
            public string CircuitName { get; set; } = default!;
            public MonthOfTheYear Month { get; set; }
            public int Year { get; set; }
            public string ChandaType { get; set; } = default!;
            public int NoOfDefaulters { get; set; }
            public PaginatedList<DefaulterMember> DefaulterMembers { get; set; } = new PaginatedList<DefaulterMember>();
        }

        public record DefaulterMember
        {
            public Guid MemberId { get; set; } = default!;
            public string MemberName { get; set; } = default!;
            public string ChandaNo { get; set; } = default!;
            public string PhoneNO { get; set; } = default!;
            public string Email { get; set; } = default!;
            public string JamaatName { get; set; } = default!;
        }

        public class Handler : IRequestHandler<Query, CircuitDefaulters>
        {
            private readonly ICurrentUser _currentUser;
            private readonly ICircuitRepository _circuitRepository;
            private readonly IChandaTypeRepository _chandaTypeRepository;
            private readonly IInvoiceItemRepository _invoiceItemRepository;

            public Handler(ICurrentUser currentUser, ICircuitRepository circuitRepository, IInvoiceItemRepository invoiceItemRepository, IChandaTypeRepository chandaTypeRepository)
            {
                _currentUser = currentUser;
                _circuitRepository = circuitRepository;
                _chandaTypeRepository = chandaTypeRepository;
                _invoiceItemRepository = invoiceItemRepository;
            }

            public async Task<CircuitDefaulters> Handle(Query request, CancellationToken cancellationToken)
            {
                var initiator = _currentUser.GetMemberDetails();
                if (initiator == null)
                {
                    throw new NotFoundException($"Please login to view report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var roles = initiator.Roles.Split(",");
                if (!roles.Any(r => r.Equals(Roles.Admin))
                    && !roles.Any(r => r.Equals(Roles.Amir))
                    && !roles.Any(r => r.Equals(Roles.ActingAmir))
                    && !roles.Any(r => r.Equals(Roles.NaibAmir))
                    && !roles.Any(r => r.Equals(Roles.NationaGenSec))
                    && !roles.Any(r => r.Equals(Roles.NationlFinSec))
                    && !roles.Any(r => r.Equals(Roles.CP))
                    && !roles.Any(r => r.Equals(Roles.CircuitFinSec)))
                {
                    throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                if(!_chandaTypeRepository.Any(ct => ct.Name == request.ChandaType))
                {
                    throw new NotFoundException($"ChandaType not exist.", ExceptionCodes.MemberNotFound.ToString(), 404);
                }

                var circuit = await _circuitRepository.GetAsync(c => c.Id == request.CircuitId);
                if (circuit == null)
                {
                    throw new NotFoundException($"Circuit not found.", ExceptionCodes.MemberNotFound.ToString(), 404);
                }

                if ((roles.Any(r => r.Equals(Roles.CP)) || roles.Any(r => r.Equals(Roles.CircuitFinSec))) && circuit.Id != initiator.CircuitId)
                {
                    throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                return await _invoiceItemRepository.GetCircuitDefaulterAsync(request.CircuitId, circuit.Name, request.ChandaType, request, request.UsePaging);
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(q => q.CircuitId)
                    .NotNull().NotEmpty().WithMessage("CircuitId is required.");
                RuleFor(q => q.ChandaType)
                    .NotNull().NotEmpty().WithMessage("ChandaType is required.");
            }
        }
    }
}
