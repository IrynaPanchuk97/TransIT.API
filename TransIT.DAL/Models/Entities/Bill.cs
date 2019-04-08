using System;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Bill : IEntity
    {
        public int Id { get; set; }
        public decimal? Sum { get; set; }
        public int? DocumentId { get; set; }
        public int? IssueId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int? CreateId { get; set; }
        public int? ModId { get; set; }

        public virtual User Create { get; set; }
        public virtual Document Document { get; set; }
        public virtual Issue Issue { get; set; }
        public virtual User Mod { get; set; }
    }
}
