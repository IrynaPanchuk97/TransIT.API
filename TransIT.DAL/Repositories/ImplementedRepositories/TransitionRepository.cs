using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class TransitionRepository:BaseRepository<Transition>, ITransitionRepository
    {
        public TransitionRepository(DbContext context)
               : base(context)
        {
        }

        protected override IQueryable<Transition> ComplexEntities => Entities.
           Include(u => u.ActionType).
           Include(u => u.FromState).
           Include(u => u.ToState).
           Include(t => t.Create).
           Include(w => w.Mod).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate);
    }
}
