﻿using System;

namespace TransIT.DAL.Models.DTOs
{
    public class IssueDTO
    {
        public int? Id { get; set; }
        public string Summary { get; set; }
        public int? Warranty { get; set; }
        public DateTime? Deadline { get; set; }
        public StateDTO State { get; set; }
        public UserDTO AssignedTo { get; set; }
        public VehicleDTO Vehicle { get; set; }
        public MalfunctionDTO Malfunction { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
    }
}
