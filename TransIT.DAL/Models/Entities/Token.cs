using System;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Token : IEntity
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int? CreateId { get; set; }
        public int? ModId { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
    }
}
