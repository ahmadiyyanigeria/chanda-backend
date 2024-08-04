using Application.Commands;
using Application.Contracts;
using Application.DTOs;
using Application.Queries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;

        public InvoicesController(IMediator mediator, IFileService fileService)
        {
            _mediator = mediator;
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceRequest request)
        {
            var command = request.Adapt<CreateInvoice.Command>();
            var invoice = await _mediator.Send(command);
            return Ok(invoice);
        }

        [HttpPost("group")]
        public async Task<IActionResult> CreateGroupInvoice([FromBody] GroupInvoiceRequest request)
        {
            var command = request.Adapt<CreateGroupInvoice.Command>();
            var invoice = await _mediator.Send(command);
            return Ok(invoice);
        }

        [HttpPost("groupFromFile")]
        public async Task<IActionResult> CreateGroupInvoiceFromFile([FromForm] InvoiceFromFileRequest request)
        {
            var invoiceItems = await _fileService.ReadInvoiceInputFromFile(request.formFile);
            var command = new CreateGroupInvoice.Command
            {
                Type = request.Type,
                InvoiceItems = invoiceItems
            };
            var invoice = await _mediator.Send(command);
            return Ok(invoice);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice(Guid id)
        {
            var query = new GetInvoice.Query(id);
            var invoice = await _mediator.Send(query);
            return Ok(invoice);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetInvoices([FromQuery] GetInvoices.Query query)
        {
            var invoice = await _mediator.Send(query);
            return Ok(invoice);
        }

       
    }
}
