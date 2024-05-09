namespace Support_Ticket_System.Entites
{
    public class TicketCategory
    {
        public Guid TicketID { get; set; }
        public Guid CategoryID { get; set; }
        public Ticket ticket { get; set; }
        public Category category { get; set; }
    }
}
