using System;
using System.Collections.Generic;

namespace TransIT.DAL.Models
{
    public partial class Bill
    {
        public int Id { get; set; }
        public decimal? Sum { get; set; }
        public int DocumentId { get; set; }
        public int IssueId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int CreateId { get; set; }
        public int ModId { get; set; }

        public virtual User Create { get; set; }
        public virtual Document Document { get; set; }
        public virtual Issue Issue { get; set; }
        public virtual User Mod { get; set; }
    }
}
