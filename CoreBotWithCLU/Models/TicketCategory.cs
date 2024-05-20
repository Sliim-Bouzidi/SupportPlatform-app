using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class TicketCategory
    {
        public Guid TicketId { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Tickets Ticket { get; set; }
    }
}
