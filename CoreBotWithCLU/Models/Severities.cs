using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Severities
    {
        public Severities()
        {
            Tickets = new HashSet<Tickets>();
        }

        public Guid SeverityId { get; set; }
        public string SeverityName { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
