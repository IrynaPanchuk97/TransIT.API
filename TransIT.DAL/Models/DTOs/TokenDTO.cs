using System;

namespace TransIT.DAL.Models.DTOs
{
    public class TokenDTO
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
