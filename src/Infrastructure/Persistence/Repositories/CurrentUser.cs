﻿using Application.DTOs;
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
            var getJamaatId = Guid.TryParse(GetClaimValue(ClaimTypes.GroupSid), out Guid jamaatId);
            var getCircuitId = Guid.TryParse(GetClaimValue(ClaimTypes.PrimaryGroupSid), out Guid circuitId);
            var getId = Guid.TryParse(GetClaimValue(ClaimTypes.PrimarySid), out Guid id);

            jamaatId = getJamaatId ? jamaatId : default;
            id = getId ? id : default;

            return new MemberDetials
            {
                Id = id,
                Name = GetClaimValue(ClaimTypes.Name),
                Email = GetClaimValue(ClaimTypes.Email),
                ChandaNo = GetClaimValue(ClaimTypes.NameIdentifier),
                JamaatId = jamaatId,
                CircuitId = circuitId,
                Roles = GetClaimValue(ClaimTypes.Role)
            };
        }

        public string GetUserId()
        {
            var getId = Guid.TryParse(GetClaimValue(ClaimTypes.PrimarySid), out Guid id);
            return getId ? id.ToString() : "";
        }

        public string GetUserNameAndChandaNo()
        {
            var name = GetClaimValue(ClaimTypes.Name);
            var chandaNo = GetClaimValue(ClaimTypes.NameIdentifier);
            return $"{name}({chandaNo})";
        }

        private string GetClaimValue(string claimType)
        {
            return User?.FindFirst(claimType)?.Value ?? string.Empty;
        }
    }
}
