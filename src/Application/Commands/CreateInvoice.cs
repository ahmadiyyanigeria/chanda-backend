using Application.Helpers;
using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Commands
{
    public class CreateInvoice
    {
        public record Command: IRequest<InvoiceResponse>
        {
            public Guid JamaatId { get; init; }
            public string InitiatorChandaNo { get; set; } = default!;
            public IReadOnlyList<InvoiceItemCommand> InvoiceItems { get; init; } = new List<InvoiceItemCommand>();
        }

        public record InvoiceResponse(Guid Id, string Reference, string JamaatName, decimal Amount, string Status, DateTime CreatedOn, IReadOnlyList<InvoiceItemResponse> InvoiceItems);

        public class Handler: IRequestHandler<Command, InvoiceResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IJamaatRepository _jamaatRepository;
            private readonly IMemberRepository _memberRepository;
            private readonly IInvoiceRepository _invoiceRepository;
            private readonly IChandaTypeRepository _chandaTypeRepository;
            private readonly CommandValidator _validator = new();

            public Handler(IUnitOfWork unitOfWork, IInvoiceRepository invoiceRepository, IJamaatRepository jamaatRepository, IMemberRepository memberRepository, IChandaTypeRepository chandaTypeRepository)
            {
                _unitOfWork = unitOfWork;
                _jamaatRepository = jamaatRepository;                
                _memberRepository = memberRepository;
                _invoiceRepository = invoiceRepository;
                _chandaTypeRepository = chandaTypeRepository;
            }

            public async Task<InvoiceResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                await _validator.ValidateAndThrowAsync(request);

                var invoiceAmount = 0.0m;
                var reference = Utility.GenerateReference("INV");

                var jamaat = await _jamaatRepository.Get(j => j.Id == request.JamaatId);
                if(jamaat is null)
                {
                    throw new DomainException($"Invalid Jamaat Id selected", ExceptionCodes.InvalidJamaat.ToString(), 400);
                }

                if (!_memberRepository.ExistsByChandaNo(request.InitiatorChandaNo))
                {
                    throw new DomainException("Initiator not recorgnised", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                Invoice invoice = new Invoice(request.JamaatId, reference, invoiceAmount, InvoiceStatus.Pending, jamaat, request.InitiatorChandaNo);

                var chandaTypeIds = request.InvoiceItems.SelectMany(ii => ii.ChandaItems.Select(ci => ci.ChandaTypeId)).ToList();
                var validChandaTypes = _chandaTypeRepository.GetChandaTypes(chandaTypeIds);

                if(validChandaTypes is null || !validChandaTypes.Any())
                {
                    throw new DomainException("No valid ChandaType selected", ExceptionCodes.NoValidChandaTypeSelected.ToString(), 400);
                }

                var payerIds = request.InvoiceItems.Select(ii => ii.PayerId).ToList();
                var payers = _memberRepository.GetMembers(m => payerIds.Contains(m.Id)).ToList();

                var invoiceItemResponses = new List<InvoiceItemResponse>();
                foreach (var item in request.InvoiceItems)
                {
                    var payer = payers.Where(p => p.Id == item.PayerId).FirstOrDefault();
                    if (payer is not null)
                    {
                        var subTotalAmount = 0.0m;
                        var invoiceItem = new InvoiceItem(item.PayerId, invoice.Id, subTotalAmount, item.MonthPaidFor, item.Year, payer, request.InitiatorChandaNo);
                        var chandaItemResponses = new List<ChandaItemResponse>();
                        foreach (var chanda in item.ChandaItems)
                        {
                            var chandaType = validChandaTypes.Where(ct => ct.Id == chanda.ChandaTypeId).FirstOrDefault();
                            if (chandaType is not null)
                            {
                                var chandaItem = new ChandaItem(invoiceItem.Id, chanda.ChandaTypeId, chanda.Amount, chandaType, request.InitiatorChandaNo);
                                invoiceItem.AddChandaItems(chandaItem);
                                subTotalAmount += chanda.Amount;

                                chandaItemResponses.Add(chandaItem.Adapt<ChandaItemResponse>());
                            }
                        }

                        if (invoiceItem.ChandaItems.Any())
                        {
                            invoiceItem.UpdateAmount(subTotalAmount);
                            invoiceAmount += invoiceItem.Amount;
                            invoice.AddInvoiceItems(invoiceItem);

                            invoiceItemResponses.Add(
                                invoiceItem.Adapt<InvoiceItemResponse>() with
                                {
                                    ChandaItems = chandaItemResponses
                                });
                        }
                    }
                }
                if (invoice.InvoiceItems.Any())
                {
                    invoice.UpdateAmount(invoiceAmount);
                }
                else
                {
                    throw new DomainException("No Valid Item selected", ExceptionCodes.NoInvoiceItemSelected.ToString(), 400);
                }

                invoice = await _invoiceRepository.AddAsync(invoice);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return invoice.Adapt<InvoiceResponse>() with
                {
                    InvoiceItems = invoiceItemResponses
                };
            }
        }

        public record InvoiceItemResponse(string PayerName, string MonthPaidFor, int Year, decimal Amount, IReadOnlyList<ChandaItemResponse> ChandaItems);

        public record ChandaItemResponse(string ChandaTypeName, decimal Amount);

        public record InvoiceItemCommand
        {
            public Guid PayerId { get; init; }
            public MonthOfTheYear MonthPaidFor {  get; init; }
            public int Year { get; init; }
            public IReadOnlyList<ChandaItemCommand> ChandaItems { get; init; } = new List<ChandaItemCommand>();
        }

        public record ChandaItemCommand(Guid ChandaTypeId, decimal Amount);

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(command => command.JamaatId)
                    .NotEmpty().WithMessage("Jamaat Id is required.");

                RuleFor(command => command.InitiatorChandaNo)
                    .NotEmpty().WithMessage("Initiator Number is required.");

                RuleFor(command => command.InvoiceItems)
                    .NotEmpty().WithMessage("No Invoice Item added.");
            }
        }
    }
}
