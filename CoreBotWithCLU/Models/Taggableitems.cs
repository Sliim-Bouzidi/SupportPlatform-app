using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Taggableitems
    {
        public Guid TicketId { get; set; }
        public Guid TagId { get; set; }

        public virtual Tags Tag { get; set; }
        public virtual Tickets Ticket { get; set; }
    }
}
