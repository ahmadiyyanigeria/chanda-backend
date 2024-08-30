using API.Authentication;
using Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMemberRepository _mem;

        public AuthController(IMemberRepository mem)
        {
            _mem = mem;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] string chandaNo)
        {
            var member = await _mem.GetMemberAsync(m => m.ChandaNo == chandaNo);
            if(member is null)
            {
                return Unauthorized("Invalid chanda number");
            }

            var token = MockJwtTokens.GenerateJwtToken(member);
            return Ok(new { token });
        }
    }
}
