using System;
using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Role : IEntity
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string TransName { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModDate { get; set; }
        public int? CreateId { get; set; }
        public int? ModId { get; set; }

        public virtual User Create { get; set; }
        public virtual User Mod { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
