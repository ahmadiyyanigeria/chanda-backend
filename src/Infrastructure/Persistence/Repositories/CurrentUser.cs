using Application.DTOs;
using Application.Repositories;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Persistence.Repositories
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public MemberDetials? GetMemberDetails()
        {
            var getId = Guid.TryParse(GetClaimValue(ClaimTypes.GroupSid), out Guid jamaatId);

            return new MemberDetials
            {
                Id = new Guid(GetClaimValue(JwtRegisteredClaimNames.Sub)),
                Name = GetClaimValue(ClaimTypes.Name),
                Email = GetClaimValue(ClaimTypes.Email),
                ChandaNo = GetClaimValue(ClaimTypes.NameIdentifier),
                JamaatId = getId ? jamaatId : default,
                Roles = GetClaimValue(ClaimTypes.Role)
            };
        }

        private string GetClaimValue(string claimType)
        {
            return User?.FindFirst(claimType)?.Value ?? string.Empty;
        }
    }
}
