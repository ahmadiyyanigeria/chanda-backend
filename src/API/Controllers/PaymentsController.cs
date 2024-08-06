using Application.Commands;
using Application.DTOs;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentsController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("{reference}")]
        public async Task<IActionResult> GetPayment(string reference)
        {
            var query = new GetPaymentByReference.Query(reference);
            var invoice = await _mediator.Send(query);
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment([FromBody] InitiatePaymentRequest model)
        {
            var command = new InitatePayment.Command { Reference = model.Reference, Option = model.Option };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string reference)
        {
            var query = new PaymentCallback.Query(reference);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
