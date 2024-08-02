using Application.DTOs;
using Microsoft.AspNetCore.Http;
using static Application.Commands.CreateGroupInvoice;

namespace Application.Contracts
{
    public interface IFileService
    {
        Task<List<InvoiceItemCommand>> ReadInvoiceInputFromFile(IFormFile formFile);
    }
}
