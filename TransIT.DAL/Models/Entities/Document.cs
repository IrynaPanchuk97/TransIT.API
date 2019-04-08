using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Document : IEntity
    {
        public Document()
        {
            Bill = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? IssueLogId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int? CreateId { get; set; }
        public int? ModId { get; set; }

        public virtual User Create { get; set; }
        public virtual IssueLog IssueLog { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<Bill> Bill { get; set; }
    }
}
