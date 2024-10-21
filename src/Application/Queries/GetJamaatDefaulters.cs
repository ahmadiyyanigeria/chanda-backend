using Application.Exceptions;
using Application.Paging;
using Application.Repositories;
using Domain.Constants;
using Domain.Enums;
using Domain.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetJamaatDefaulters
    {
        public record Query(Guid JamaatId, string ChandaType, bool UsePaging): PageRequest, IRequest<JamaatDefaulter>;

        public record JamaatDefaulter
        {
            public Guid JamaatId { get; set; }
            public string JamaatName { get; set; } = default!;
            public MonthOfTheYear Month {  get; set; }
            public int Year { get; set; }
            public string ChandaType { get; set; } = default!;
            public int NoOfDefaulters { get; set; }
            public PaginatedList<Defaulter> Defaulters { get; set; } = new PaginatedList<Defaulter>();
        }

        public record Defaulter
        {
            public Guid MemberId { get; set; } = default!;
            public string MemberName { get; set; } = default!;
            public string ChandaNo { get; set; } = default!;
            public string PhoneNO { get; set; } = default!;
            public string Email { get; set; } = default!;
        }

        public class Handler : IRequestHandler<Query, JamaatDefaulter>
        {
            private readonly ICurrentUser _currentUser;
            private readonly IJamaatRepository _jamaatRepository;
            private readonly IInvoiceItemRepository _invoiceItemRepository;

            public Handler(ICurrentUser currentUser, IJamaatRepository jamaatRepository, IInvoiceItemRepository invoiceItemRepository)
            {
                _currentUser = currentUser;
                _jamaatRepository = jamaatRepository;
                _invoiceItemRepository = invoiceItemRepository;
            }

            public async Task<JamaatDefaulter> Handle(Query request, CancellationToken cancellationToken)
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
                    && !roles.Any(r => r.Equals(Roles.CircuitFinSec))
                    && !roles.Any(r => r.Equals(Roles.JamaatPresident))
                    && !roles.Any(r => r.Equals(Roles.JamaatFinSec)))
                {
                    throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                if ((roles.Any(r => r.Equals(Roles.JamaatPresident)) || roles.Any(r => r.Equals(Roles.JamaatFinSec))) && request.JamaatId != initiator.JamaatId)
                {
                    throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var jamaat = await _jamaatRepository.GetAsync(j => j.Id == request.JamaatId);
                if (jamaat == null)
                {
                    throw new NotFoundException($"Jamaat not found.", ExceptionCodes.MemberNotFound.ToString(), 404);
                }

                if ((roles.Any(r => r.Equals(Roles.CP)) || roles.Any(r => r.Equals(Roles.CircuitFinSec))) && jamaat.CircuitId != initiator.CircuitId)
                {
                    throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                return await _invoiceItemRepository.GetJamaatDefaulterAsync(request.JamaatId, request.ChandaType, request, request.UsePaging);
            }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(q => q.JamaatId)
                    .NotNull().NotEmpty().WithMessage("JamaatId is required.");
                RuleFor(q => q.ChandaType)
                    .NotNull().NotEmpty().WithMessage("ChandaType is required.");
            }
        }
    }
}
