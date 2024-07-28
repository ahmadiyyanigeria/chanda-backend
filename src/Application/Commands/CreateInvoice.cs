using Application.Exceptions;
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
                var invoiceAmount = 0.0m;
                var invoiceId = Guid.NewGuid();
                var reference = Utility.GenerateReference("INV");

                var jamaat = await _jamaatRepository.Get(j => j.Id == request.JamaatId);
                if(jamaat is null)
                {
                    throw new NotFoundException($"Invalid Jamaat Id selected", ExceptionCodes.InvalidJamaat.ToString(), 400);
                }

                if (!_memberRepository.ExistsByChandaNo(request.InitiatorChandaNo))
                {
                    throw new NotFoundException("Initiator not recorgnised", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var chandaTypeIds = request.InvoiceItems.SelectMany(ii => ii.ChandaItems.Select(ci => ci.ChandaTypeId)).ToList();
                var validChandaTypes = _chandaTypeRepository.GetChandaTypes(chandaTypeIds);

                if(validChandaTypes is null || !validChandaTypes.Any())
                {
                    throw new NotFoundException("No valid ChandaType selected", ExceptionCodes.NoValidChandaTypeSelected.ToString(), 400);
                }

                var payerIds = request.InvoiceItems.Select(ii => ii.PayerId).ToList();
                var payers = _memberRepository.GetMembers(m => payerIds.Contains(m.Id)).ToList();

                var invoiceItems = new List<InvoiceItem>();
                var invoiceItemResponses = new List<InvoiceItemResponse>();
                foreach (var item in request.InvoiceItems)
                {
                    var payer = payers.Where(p => p.Id == item.PayerId).FirstOrDefault();
                    if (payer is not null)
                    {
                        var invoiceItemAmount = 0.0m;
                        var invoiceItemId = Guid.NewGuid();
                        var chandaItems = new List<ChandaItem>();
                        var chandaItemResponses = new List<ChandaItemResponse>();
                        foreach (var chanda in item.ChandaItems)
                        {
                            var chandaType = validChandaTypes.Where(ct => ct.Id == chanda.ChandaTypeId).FirstOrDefault();
                            if (chandaType is not null)
                            {
                                var chandaItem = new ChandaItem(invoiceItemId, chanda.ChandaTypeId, chanda.Amount, chandaType, request.InitiatorChandaNo);
                                chandaItems.Add(chandaItem);
                                invoiceItemAmount += chanda.Amount;

                                chandaItemResponses.Add(chandaItem.Adapt<ChandaItemResponse>());
                            }
                        }

                        if (chandaItems.Any())
                        {
                            var invoiceItem = new InvoiceItem(invoiceItemId, item.PayerId, invoiceId, invoiceItemAmount, item.MonthPaidFor, item.Year, payer, request.InitiatorChandaNo);
                            
                            invoiceAmount += invoiceItemAmount;
                            invoiceItems.Add(invoiceItem);

                            invoiceItemResponses.Add(
                                invoiceItem.Adapt<InvoiceItemResponse>() with
                                {
                                    ChandaItems = chandaItemResponses
                                });
                        }
                    }
                }
                if (!invoiceItems.Any())
                {
                    throw new NotFoundException("No Valid Item selected", ExceptionCodes.NoInvoiceItemSelected.ToString(), 400);
                }

                var invoice = new Invoice(invoiceId, request.JamaatId, reference, invoiceAmount, InvoiceStatus.Pending, jamaat, request.InitiatorChandaNo);
                
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
                    .NotEmpty().WithMessage("Initiator Chanda number is required.");

                RuleFor(command => command.InvoiceItems)
                    .NotEmpty().WithMessage("No Invoice Item added.");
            }
        }
    }
}
