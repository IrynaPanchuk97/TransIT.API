using System;
using System.Collections.Generic;

namespace TransIT.Models.Entities
{
    public partial class State
    {
        public State()
        {
            Issue = new HashSet<Issue>();
            IssueLogNewState = new HashSet<IssueLog>();
            IssueLogOldState = new HashSet<IssueLog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Issue> Issue { get; set; }
        public virtual ICollection<IssueLog> IssueLogNewState { get; set; }
        public virtual ICollection<IssueLog> IssueLogOldState { get; set; }
    }
}
