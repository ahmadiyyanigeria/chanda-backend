using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("{reference}")]
        public async Task<IActionResult> GetPayment(string reference)
        {
            var query = new GetPaymentByReference.Query(reference);
            var invoice = await _mediator.Send(query);
            return Ok(invoice);
        }
    }
}
