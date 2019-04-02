﻿using System;
using System.Collections.Generic;

namespace TransIT.DAL.Models.Entities
{
    public partial class Supplier
    {
        public Supplier()
        {
            IssueLog = new HashSet<IssueLog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int? CreateId { get; set; }
        public int? ModId { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<IssueLog> IssueLog { get; set; }
    }
}
