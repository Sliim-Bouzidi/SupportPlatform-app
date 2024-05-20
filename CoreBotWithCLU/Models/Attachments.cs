using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Attachments
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public Guid TicketId { get; set; }

        public virtual Tickets Ticket { get; set; }
    }
}
