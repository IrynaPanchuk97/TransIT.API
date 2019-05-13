using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class Country : IEntity
    {
        public Country()
        {
            Supplier = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CreateId { get; set; }
        public int? ModId { get; set; }

        public virtual ICollection<Supplier> Supplier { get; set; }
    }
}
