using BusinessLayer.Extentions;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationOptions = BusinessLayer.Extentions.AuthenticationOptions;

namespace BusinessLayer.Services
{
    public class TokensService
    {
        private readonly UsersRepository _userRepository;

        private readonly IOptions<AuthenticationOptions> _authOptions;

        public TokensService(UsersRepository userRepository, IOptions<AuthenticationOptions> authOptions)
        {
            _userRepository = userRepository;
            _authOptions = authOptions;
        }

        private async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
        };

            var role = await _userRepository.GetUserRoleAsync(user);
            claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }

        private SecurityTokenDescriptor GetTokenDescriptor(List<Claim> claims, SigningCredentials creds)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Audience = _authOptions.Value.Audience,
                Issuer = _authOptions.Value.Issuer,
            };
            return tokenDescriptor;
        }

        public async Task<string> GetTokenAsync(User user)
        {
            var claims = await GetClaimsAsync(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Value.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = GetTokenDescriptor(claims, creds);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
