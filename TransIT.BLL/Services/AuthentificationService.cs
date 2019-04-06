using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TransIT.BLL.Security;
using TransIT.BLL.Security.Hashers;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly TransITDBContext _transITDBContext;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenOptions _tokenOptions;
        private readonly IPasswordHasher _passwordHasher;
        public AuthentificationService(
            TransITDBContext transITDBContext,
            SigningConfigurations signingConfigurations,
            TokenOptions tokenOptions,
            IPasswordHasher passwordHasher)
        {
            _transITDBContext=transITDBContext;
            _signingConfigurations = signingConfigurations;
            _tokenOptions = tokenOptions;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Authentificate(string login, string password)
        {
            User user = await _transITDBContext.User.SingleOrDefaultAsync(u => u.Login == login);
            if (user == null || !_passwordHasher.CheckMatch(password, user.Password))
            {
                return null;
            }
            

            var token = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: GetClaims(user),
                expires: DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration),
                notBefore: DateTime.UtcNow,
                signingCredentials: _signingConfigurations.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Login)
            };

            claims.Add(new Claim(ClaimTypes.Role,user.Role.ToString())); //

            return claims;
        }
    }
}
