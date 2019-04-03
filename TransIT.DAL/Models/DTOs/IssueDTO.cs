using System;

namespace TransIT.DAL.Models.DTOs
{
    public class IssueDTO
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public int? Warranty { get; set; }
        public DateTime? Deadline { get; set; }
        public StateDTO StateId { get; set; }
        public int? AssignedTo { get; set; }
        public VehicleDTO VehicleId { get; set; }
        public MalfunctionDTO MalfunctionId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
    }
}
