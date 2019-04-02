using System;

namespace TransIT.DAL.Models.DTOs
{
    public class BillDTO
    {
        public int Id { get; set; }
        public decimal? Sum { get; set; }
        public int? DocumentId { get; set; }
        public int? IssueId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
    }
}
