using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TransIT.BLL.Helpers.Abstractions;
using TransIT.DAL.Models.DTOs;

namespace TransIT.BLL.Helpers
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }
        
        public Task<(ClaimsPrincipal principal, JwtSecurityToken jwt)> GetPrincipalFromExpiredTokenAsync(string token) =>
            Task.Run(() => 
            {
                var principal = new JwtSecurityTokenHandler()
                    .ValidateToken(
                        token,
                        new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = _jwtOptions.SigningCredentials.Key,
                            ValidateLifetime = false
                        },
                        out var securityToken);
    
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null
                    || !jwtSecurityToken.Header.Alg.Equals(
                        SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                
                return (principal, jwtSecurityToken);
            });
        
        private Task<string> GenerateAccessTokenAsync(int userId, string email, string role) =>
            Task.Run(() => new JwtSecurityTokenHandler()
                .WriteToken(new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: GenerateClaims(userId, email, role),
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtOptions.AccessExpirationMins)),
                    signingCredentials: _jwtOptions.SigningCredentials
            )));
        
        private Task<string> GenerateRefreshTokenAsync(int userId, string email, string role) =>
            Task.Run(() => new JwtSecurityTokenHandler()
                .WriteToken(new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: GenerateClaims(userId, email, role),
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtOptions.RefreshExpirationMins)),
                    signingCredentials: _jwtOptions.SigningCredentials
            )));
        
        public Task<TokenDTO> GenerateTokenAsync(int userId, string email, string role) =>
            Task.Run(async () => new TokenDTO
                {
                    AccessToken = await GenerateAccessTokenAsync(userId, email, role),
                    RefreshToken = await GenerateRefreshTokenAsync(userId, email, role)
                });

        private Claim[] GenerateClaims(int userId, string email, string role) =>
            new[]
            {
                new Claim(JwtRegisteredClaimNames.Iss, email),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(nameof(role), role),
                new Claim(JwtRegisteredClaimNames.Jti, _jwtOptions.JtiGenerator),
                new Claim(JwtRegisteredClaimNames.Iat, 
                    ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64)
            };
        
        private long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round(
                (date.ToUniversalTime() - new DateTimeOffset(
                     1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
        
        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
        }
    }
}
