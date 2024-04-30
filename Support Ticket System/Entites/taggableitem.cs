namespace Support_Ticket_System.Entites
{
    public class taggableitem
    {
        public Guid TicketID { get; set; }
        public Guid TagID { get; set; }
        
        public Ticket ticket { get; set; }
        public Tag tag { get; set; }
    }
}
