namespace Support_Ticket_System.Entites
{
    public class StatusHistory
    {
        public Guid StatusHistoryID { get; set; }
       
        public string? StatusValue { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid StatusID { get; set; }
        public Status status { get; set; }
        public Guid TicketID { get; set; }
        public Ticket Ticket { get; set; }
    }
}
