using System;
using System.Collections.Generic;
using System.Text;

namespace TransIT.DAL.Models.Entities.Abstractions
{
    public interface IEntity
    {
        int Id { get; set; }
        int? CreateId { get; set; }
        int? ModId { get; set; }
    }
}
