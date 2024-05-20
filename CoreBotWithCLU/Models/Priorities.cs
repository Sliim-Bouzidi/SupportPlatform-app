using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Priorities
    {
        public Priorities()
        {
            Tickets = new HashSet<Tickets>();
        }

        public Guid PriorityId { get; set; }
        public string PriorityName { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
