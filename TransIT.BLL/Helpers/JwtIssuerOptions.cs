using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace TransIT.BLL.Helpers
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public int AccessExpirationMins { get; set; }
        public int RefreshExpirationMins { get; set; }
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);
        public string JtiGenerator => Guid.NewGuid().ToString();
        public SigningCredentials SigningCredentials { get; set; }
    }
}