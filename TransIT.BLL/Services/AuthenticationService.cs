using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TransIT.BLL.Helpers.Abstractions;
using TransIT.BLL.Security.Hashers;
using TransIT.BLL.Services.Abstractions;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Models.ViewModels;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtFactory _jwtFactory;
        
        public AuthenticationService(
            ILogger<AuthenticationService> logger,
            IPasswordHasher hasher,
            IJwtFactory jwtFactory,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hasher = hasher;
            _jwtFactory = jwtFactory;
        }
        
        public async Task<TokenDTO> SignInAsync(LoginViewModel credentials)
        {
            var user = (await _unitOfWork.UserRepository
                .GetAllAsync(u => u.Login == credentials.Login))
                .SingleOrDefault();

            if (user != null && _hasher.CheckMatch(credentials.Password, user.Password))
            {
                var token = await _jwtFactory.GenerateTokenAsync(user.Id, user.Email, user.Role.Name);

                if (token == null) return null;
                
                await _unitOfWork.TokenRepository.AddAsync(new Token
                {
                    RefreshToken = token.RefreshToken,
                    CreateId = user.Id,
                    Create = user
                });
                await _unitOfWork.SaveAsync();

                return token;
            }

            return null;
        }

        public async Task<TokenDTO> TokenAsync(TokenDTO token)
        {
            try
            {
                var principal = await _jwtFactory.GetPrincipalFromExpiredTokenAsync(token.AccessToken);

                if (principal == null ||
                    !int.TryParse(principal.FindFirst(JwtRegisteredClaimNames.Sub).Value, out var id))
                    return null;

                var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

                if (user == null
                    || user.Login == principal.FindFirst(JwtRegisteredClaimNames.Iss)?.Value
                    || user.Role.Name == principal.FindFirst("role")?.Value) return null;
                
                var refreshToken = (await _unitOfWork.TokenRepository.GetAllAsync(t =>
                    (int)t.CreateId == id
                    && t.RefreshToken == token.RefreshToken))
                    .SingleOrDefault();

                if (refreshToken == null) return null;

                var newToken = await _jwtFactory.GenerateTokenAsync(
                    user.Id, 
                    user.Login,
                    user.Role.Name);

                _unitOfWork.TokenRepository.Remove(refreshToken);
                await _unitOfWork.TokenRepository.AddAsync(new Token
                {
                    RefreshToken = newToken.RefreshToken,
                    CreateId = user.Id,
                    Create = user
                });
                await _unitOfWork.SaveAsync();
                

                return newToken;
            }
            catch (SecurityTokenException e)
            {
                return null;
            }
        }
        
        /*
                new Claim(JwtRegisteredClaimNames.Iss, email),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(nameof(role), role),
                new Claim(JwtRegisteredClaimNames.Jti, _jwtOptions.JtiGenerator),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64)
         */
    }
}