using System.Collections.Generic;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.DAL.Models.Entities
{
    public partial class State : IEntity
    {
        public State()
        {
            Issue = new HashSet<Issue>();
            IssueLogNewState = new HashSet<IssueLog>();
            IssueLogOldState = new HashSet<IssueLog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string TransName { get; set; }
        public virtual ICollection<Issue> Issue { get; set; }
        public virtual ICollection<IssueLog> IssueLogNewState { get; set; }
        public virtual ICollection<IssueLog> IssueLogOldState { get; set; }
    }
}
