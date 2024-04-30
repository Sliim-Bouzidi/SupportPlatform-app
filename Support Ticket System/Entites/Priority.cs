using System.ComponentModel.DataAnnotations;

namespace Support_Ticket_System.Entites
{
    public class Priority
    {
        public Guid PriorityID { get; set; }
        
        public string PriorityName { get; set; }
        
        
    }
}
