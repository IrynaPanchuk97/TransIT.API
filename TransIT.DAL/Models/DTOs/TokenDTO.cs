using System;

namespace TransIT.DAL.Models.DTOs
{
    public class TokenDTO
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
    }
}
