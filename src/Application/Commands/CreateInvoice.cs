using Application.Exceptions;
using Application.Helpers;
using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Commands
{
    public class CreateInvoice
    {
        public record Command: IRequest<InvoiceResponse>
        {
            public string JamaatCode { get; init; } = default!;
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
                var reference = Utility.GenerateReference("MEM");

                var jamaat = await _jamaatRepository.Get(j => j.Code == request.JamaatCode);
                if(jamaat is null)
                {
                    throw new NotFoundException($"Invalid Jamaat selected", ExceptionCodes.InvalidJamaat.ToString(), 400);
                }

                if (!_memberRepository.ExistsByChandaNo(request.InitiatorChandaNo))
                {
                    throw new NotFoundException("Initiator not recorgnised", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var chandaTypeCodes = request.InvoiceItems.SelectMany(ii => ii.ChandaItems.Select(ci => ci.ChandaTypeCode)).ToList();
                var validChandaTypes = _chandaTypeRepository.GetChandaTypes(chandaTypeCodes);

                if(validChandaTypes is null || !validChandaTypes.Any())
                {
                    throw new NotFoundException("No valid ChandaType selected", ExceptionCodes.NoValidChandaTypeSelected.ToString(), 400);
                }

                var payerNos = request.InvoiceItems.Select(ii => ii.PayerNo).ToList();
                var payers = _memberRepository.GetMembers(m => payerNos.Contains(m.ChandaNo)).ToList();

                var invoiceItems = new List<InvoiceItem>();
                var invoiceItemResponses = new List<InvoiceItemResponse>();
                foreach (var item in request.InvoiceItems)
                {
                    var payer = payers.Where(p => p.ChandaNo == item.PayerNo).FirstOrDefault();
                    if (payer is not null)
                    {
                        var invoiceItemAmount = 0.0m;
                        var invoiceItemId = Guid.NewGuid();
                        var chandaItems = new List<ChandaItem>();
                        var chandaItemResponses = new List<ChandaItemResponse>();
                        foreach (var chanda in item.ChandaItems)
                        {
                            var chandaType = validChandaTypes.Where(ct => ct.Code == chanda.ChandaTypeCode).FirstOrDefault();
                            if (chandaType is not null)
                            {
                                var chandaItem = new ChandaItem(invoiceItemId, chandaType.Id, chanda.Amount, request.InitiatorChandaNo);
                                chandaItems.Add(chandaItem);
                                invoiceItemAmount += chanda.Amount;

                                chandaItemResponses.Add(new ChandaItemResponse(chandaType.Name, chanda.Amount));
                            }
                        }

                        if (chandaItems.Any())
                        {
                            var invoiceItem = new InvoiceItem(invoiceItemId, payer.Id, invoiceId, invoiceItemAmount, item.MonthPaidFor, item.Year, request.InitiatorChandaNo);
                            invoiceItem.AddChandaItems(chandaItems);
                            invoiceAmount += invoiceItemAmount;
                            invoiceItems.Add(invoiceItem);

                            invoiceItemResponses.Add( new InvoiceItemResponse(payer.Name, item.MonthPaidFor.ToString(), item.Year, invoiceItemAmount, chandaItemResponses));
                        }
                    }
                }
                if (!invoiceItems.Any())
                {
                    throw new NotFoundException("No Valid Item selected", ExceptionCodes.NoInvoiceItemSelected.ToString(), 400);
                }

                var invoice = new Invoice(invoiceId, jamaat.Id, reference, invoiceAmount, InvoiceStatus.Pending, request.InitiatorChandaNo);
                invoice.AddInvoiceItems(invoiceItems);
                
                invoice = await _invoiceRepository.AddAsync(invoice);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                return new InvoiceResponse(invoiceId, reference, jamaat.Name, invoiceAmount, invoice.Status.ToString(), invoice.CreatedOn, invoiceItemResponses);
            }
        }

        public record InvoiceItemResponse(string PayerName, string MonthPaidFor, int Year, decimal Amount, IReadOnlyList<ChandaItemResponse> ChandaItems);

        public record ChandaItemResponse(string ChandaTypeName, decimal Amount);

        public record InvoiceItemCommand
        {
            public string PayerNo { get; init; } = default!;
            public MonthOfTheYear MonthPaidFor {  get; init; }
            public int Year { get; init; }
            public IReadOnlyList<ChandaItemCommand> ChandaItems { get; init; } = new List<ChandaItemCommand>();
        }

        public record ChandaItemCommand(string ChandaTypeCode, decimal Amount);

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(command => command.JamaatCode)
                    .NotEmpty().WithMessage("Jamaat Id is required.");

                RuleFor(command => command.InitiatorChandaNo)
                    .NotEmpty().WithMessage("Initiator Chanda number is required.");

                RuleFor(command => command.InvoiceItems)
                    .NotEmpty().WithMessage("No Invoice Item added.");
            }
        }
    }
}
