using System;

namespace TransIT.DAL.Models.DTOs
{
    public class MalfunctionSubgroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? MalfunctionGroupId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
    }
}
