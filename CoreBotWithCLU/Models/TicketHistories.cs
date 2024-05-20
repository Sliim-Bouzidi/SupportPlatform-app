using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class TicketHistories
    {
        public Guid TicketHistoryId { get; set; }
        public string ChangeType { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid TicketId { get; set; }
        public Guid? UserId { get; set; }

        public virtual Tickets Ticket { get; set; }
        public virtual Users User { get; set; }
    }
}
