using System;

namespace TransIT.DAL.Models.DTOs
{
    public class IssueDTO
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public int? Warranty { get; set; }
        public DateTime? Deadline { get; set; }
        public int? StateId { get; set; }
        public int? AssignedTo { get; set; }
        public int? VehicleId { get; set; }
        public int? MalfunctionId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
    }
}
