namespace Support_Ticket_System.DTOs
{
    public class UpdateTicketDto
    {
        
        
        public string? title { get; set; } 
        public string? description { get; set; }
        public string? assignTo { get; set; }
        public string? statusName { get; set; }
        public List<string>? Tags { get; set; }
    }
}
