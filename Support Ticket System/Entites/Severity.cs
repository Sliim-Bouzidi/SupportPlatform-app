using System.ComponentModel.DataAnnotations;

namespace Support_Ticket_System.Entites
{
    public class Severity
    {
        public Guid SeverityID { get; set; }
        
        public string SeverityName{ get; set; }
        
    }
}
