using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class TicketType
    {
        public TicketType()
        {
            Tickets = new HashSet<Tickets>();
        }

        public Guid TicketTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
