namespace Support_Ticket_System.Entites
{
    public class Reason
    {
        public Guid ReasonID { get; set; }
        public string Content { get; set; }
        public Ticket  Ticket { get; set; }
    }
}
