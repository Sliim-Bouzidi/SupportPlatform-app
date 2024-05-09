namespace Support_Ticket_System.Entites
{
    public class Category
    {
        public Guid CategoryID { get; set; }
        public string Name { get; set; }
        public ICollection<TicketCategory>? categories { get; set; }
    }
}
