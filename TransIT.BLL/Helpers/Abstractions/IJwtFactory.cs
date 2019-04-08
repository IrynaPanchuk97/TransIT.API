using System.Security.Claims;
using System.Threading.Tasks;
using TransIT.DAL.Models.DTOs;

namespace TransIT.BLL.Helpers.Abstractions
{
    /// <summary>
    /// Behavior of jwt
    /// </summary>
    public interface IJwtFactory
    {
        /// <summary>
        /// Exctracts user info from token
        /// </summary>
        /// <param name="token">Token string</param>
        /// <returns>User info</returns>
        Task<ClaimsPrincipal> GetPrincipalFromExpiredTokenAsync(string token);
        /// <summary>
        /// Generates new token
        /// </summary>
        /// <param name="email">Email of user in token</param>
        /// <param name="role">Role of user in token</param>
        /// <returns>Entity token</returns>
        Task<TokenDTO> GenerateTokenAsync(int userId, string email, string role);
    }
}
