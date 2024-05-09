using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Support_Ticket_System.DTOs
{
    public class CreateTicketDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public string assignTo { get; set; }
       public string statusName { get; set; }
        public string SeverityName { get; set; }
        public string PriorirtyName { get; set; }
        public List<string> Tags { get; set; }
        public List<string> categories { get; set; }
        public string processflowName { get; set; }
        public string tickettype { get; set; }
        
        public string username { get; set; }
        
        public string tenantname { get; set; }


    }
}
