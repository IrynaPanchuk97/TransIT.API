using System.Threading.Tasks;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.BLL.Services.Abstractions
{
    /// <summary>
    /// Behavior of authentication
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Checks credential
        /// </summary>
        /// <param name="credentials">User login and password</param>
        /// <returns>Access and refresh tokens</returns>
        Task<TokenDTO> SignInAsync(LoginViewModel credentials);
        /// <summary>
        /// Renews a token
        /// </summary>
        /// <param name="token">Old tokens</param>
        /// <returns>New tokens</returns>
        Task<TokenDTO> TokenAsync(TokenDTO token);
    }
}