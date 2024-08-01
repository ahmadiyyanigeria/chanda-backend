using Application.Contracts;
using Application.Exceptions;
using Application.Repositories;
using CsvHelper;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using static Application.Commands.CreateGroupInvoice;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IChandaTypeRepository _typeRepository;

        public FileService(IChandaTypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public async Task<List<InvoiceItemCommand>> ReadInvoiceInputFromFile(IFormFile formFile)
        {
            if(formFile is null || formFile.Length <= 0)
            {
                throw new BadRequestException("Invalid file attached", ExceptionCodes.InvalidFile.ToString(), 400);
            }

            var typeNames = await _typeRepository.GetAllNamesAsync();
            if(typeNames is null || !typeNames.Any())
            {
                throw new NotFoundException("No Chanda Type found.", ExceptionCodes.ChandaTypeNotFound.ToString(), 404);
            }

            using var stream = formFile.OpenReadStream();
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Read();
            csv.ReadHeader();
            var headers = csv.HeaderRecord;
            if(headers is null || headers.Length <= 0)
            {
                throw new BadRequestException("Invalid file attached.", ExceptionCodes.InvalidFile.ToString(), 400);
            }

            var invoiceItems = new List<InvoiceItemCommand>();
            try
            {
                while (csv.Read())
                {
                    var chandaNo = csv.GetField<string>("ChandaNo")!;
                    var receiptNo = csv.GetField<string>("ReceiptNo")!;
                    var monthPaidFor = csv.GetField<string>("MonthPaidFor");
                    var year = csv.GetField<int>("Year");

                    var chandaItems = new List<ChandaItemCommand>();
                    foreach (var header in headers)
                    {
                        if (typeNames.Any(n => n.Equals(header, StringComparison.OrdinalIgnoreCase)))
                        {
                            var amount = csv.GetField<decimal?>(header);
                            if (amount.HasValue && amount.Value > 0)
                            {
                                chandaItems.Add(new ChandaItemCommand(header, amount.Value));
                            }
                        }
                    }

                    invoiceItems.Add(new InvoiceItemCommand
                    {
                        ChandaNo = chandaNo,
                        ReceiptNo = receiptNo,
                        MonthPaidFor = Enum.TryParse<MonthOfTheYear>(monthPaidFor, out var month) ? month : default,
                        Year = year,
                        ChandaItems = chandaItems
                    });
                }
            }catch(Exception ex)
            {
                //Todo
                //log exception message.
                throw new BadRequestException("File is not valid.", ExceptionCodes.InvalidFile.ToString(), 400);
            }

            return invoiceItems;
        }

    }
}
