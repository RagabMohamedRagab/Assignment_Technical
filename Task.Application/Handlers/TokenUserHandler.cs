using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Task.Application.Dtos;
using Task.Application.Queries;
using Task.Core.Entities;

namespace Task.Application.Handlers
{
    public class TokenUserHandler(IHttpContextAccessor httpContextAccessor, IOptions<Jwt> options, UserManager<user> userManager) : IRequestHandler<TokenUserQuery, AuthUserDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly Jwt _jwtOptions = options.Value;
        private readonly UserManager<user> _userManager = userManager;
        public async Task<AuthUserDto> Handle(TokenUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);

                if (user == null) return new AuthUserDto { IsAuthenticated = false, Token = string.Empty };

                var claim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
               };
                var claims = new[]
                {
                    new Claim("Id", user.Id ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Name, user.Name ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                }.Union(claim);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(_jwtOptions.Expiries),
                    signingCredentials: credentials);

                return new AuthUserDto
                {
                    IsAuthenticated = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }
            catch (Exception ex)
            {
                return new AuthUserDto { IsAuthenticated = false, Token = string.Empty };
            }

        }
    }
}
