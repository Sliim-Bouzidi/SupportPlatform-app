using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Comments
    {
        public Guid CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid TicketId { get; set; }

        public virtual Tickets Ticket { get; set; }
        public virtual Users User { get; set; }
    }
}
