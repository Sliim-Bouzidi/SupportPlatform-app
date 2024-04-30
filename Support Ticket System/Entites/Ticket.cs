using System.Text.Json.Serialization;

namespace Support_Ticket_System.Entites
{
    public class Ticket
    {
        public Guid TicketID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set;}
        public DateTime UpdatedDate { get; set;}
        public string? AssignTo { get; set; }
        public string? Status { get; set; }
        public Guid ? UserID { get; set; }
        public User ? user { get; set; }
        public Tenant? tenant { get; set; }
        public Priority ? priority { get; set; }
        public Severity  ? severity { get; set; }
        public ICollection<TicketHistory> ? ticketHistories { get; set; }
        public Guid ? ProcessFlowId { get; set; }
        public ProcessFlow ? processFlow { get; set; }
        public ICollection<taggableitem>? tags { get; set; }
    }
}
