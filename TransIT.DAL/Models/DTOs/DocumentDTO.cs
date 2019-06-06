﻿using System;
using Microsoft.AspNetCore.Http;
namespace TransIT.DAL.Models.DTOs
{
    public class DocumentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IssueLogDTO IssueLog { get; set; }
        public IFormFile File { get; set; }
        public UserDTO Mod { get; set; }
        public UserDTO Create { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public string Path { get; set; }
    }
}
