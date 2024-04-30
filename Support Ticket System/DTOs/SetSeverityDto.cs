using System.ComponentModel.DataAnnotations;

namespace Support_Ticket_System.DTOs
{
    public class SetSeverityDto
    {
        [Required]
        public string SeverityName { get; set; }
    }
}
