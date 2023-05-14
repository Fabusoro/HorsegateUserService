using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagement.Api.Domain;

namespace UserManagement.Api.Services.Interface
{
    public interface ITokenService
    {
        string GenerateRefreshToken();
        Task<string> GenerateToken(Users user);
    }
}