using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Statushistory
    {
        public Guid StatusHistoryId { get; set; }
        public string StatusValue { get; set; }
        public Guid StatusId { get; set; }
        public Guid TicketId { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Statuses Status { get; set; }
        public virtual Tickets Ticket { get; set; }
    }
}
