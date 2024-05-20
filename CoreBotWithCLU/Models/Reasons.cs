using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Reasons
    {
        public Guid ReasonId { get; set; }
        public string Content { get; set; }
        public Guid TicketId { get; set; }

        public virtual Tickets Ticket { get; set; }
    }
}
