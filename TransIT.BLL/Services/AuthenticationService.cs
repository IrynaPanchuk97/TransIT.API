using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                var user = (await _unitOfWork.UserRepository
                    .GetAllAsync(u => u.Login == credentials.Login))
                    .SingleOrDefault();

                if (user != null && _hasher.CheckMatch(credentials.Password, user.Password))
                {
                    var role = await _unitOfWork.RoleRepository.GetByIdAsync((int) user.RoleId);
                    if (role == null) return null;

                    var token = await _jwtFactory.GenerateTokenAsync(user.Id, user.Login, role.Name);
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
            catch (Exception e)
            {
                _logger.LogError(e, nameof(TokenAsync));
                throw e;
            }
        }

        public async Task<TokenDTO> TokenAsync(TokenDTO token)
        {
            try
            {
                var principal = await _jwtFactory.GetPrincipalFromExpiredTokenAsync(token.AccessToken);
                var user = await _unitOfWork.UserRepository.GetByIdAsync(int.Parse(principal.jwt.Subject));
                var role = await _unitOfWork.RoleRepository.GetByIdAsync((int) user.RoleId);

                _unitOfWork.TokenRepository.Remove(
                    (await _unitOfWork.TokenRepository.GetAllAsync(t =>
                        (int) t.CreateId == user.Id
                        && t.RefreshToken == token.RefreshToken))
                    .SingleOrDefault());

                var newToken = await _jwtFactory.GenerateTokenAsync(
                    user.Id, user.Login, role.Name);
                await _unitOfWork.TokenRepository.AddAsync(new Token
                {
                    RefreshToken = newToken.RefreshToken,
                    CreateId = user.Id,
                    Create = user
                });
                await _unitOfWork.SaveAsync();
                return newToken;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, nameof(TokenAsync));
                return null;
            }
            catch (SecurityTokenException e)
            {
                _logger.LogError(e, nameof(TokenAsync));
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(TokenAsync));
                throw e;
            }
        }
    }
}
