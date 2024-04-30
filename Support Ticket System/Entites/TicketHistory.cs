namespace Support_Ticket_System.Entites
{
    public class TicketHistory
    {
        public Guid TicketHistoryID { get; set; }
        public string changeType { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid TicketID { get; set; }
        public Ticket Ticket { get; set; }
        public Guid? UserID { get; set; }
        public User user { get; set; }

    }
}
