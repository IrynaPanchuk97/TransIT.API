using System;

namespace TransIT.DAL.Models.DTOs
{
    public class IssueLogDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? Expenses { get; set; }
        public StateDTO OldStateId { get; set; }
        public StateDTO NewStateId { get; set; }
        public SupplierDTO SupplierId { get; set; }
        public ActionTypeDTO ActionTypeId { get; set; }
        public IssueDTO IssueId { get; set; }
        public int? CreateId { get; set; }
        public int? ModId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
    }
}
