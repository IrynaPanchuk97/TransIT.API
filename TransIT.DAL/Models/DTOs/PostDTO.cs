﻿using System;

namespace TransIT.DAL.Models.DTOs
{
    public class PostDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
    }
}
