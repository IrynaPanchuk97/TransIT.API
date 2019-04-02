using System;

namespace TransIT.DAL.Models.DTOs
{
    public class IssueLogDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? Expenses { get; set; }
        public int? OldStateId { get; set; }
        public int? NewStateId { get; set; }
        public int? SupplierId { get; set; }
        public int? ActionTypeId { get; set; }
        public int? IssueId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
    }
}
