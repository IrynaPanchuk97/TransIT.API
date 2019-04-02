using System;

namespace TransIT.DAL.Models.DTOs
{
    public class MalfunctionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? MalfunctionSubgroupId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
    }
}
