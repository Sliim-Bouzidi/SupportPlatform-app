using System.ComponentModel.DataAnnotations;

namespace Support_Ticket_System.Entites
{
    public class ProcessFlowAttrribut
    {

        [Key]
        public Guid PFAttributID { get; set; }
        public string? Value { get; set; }
 
        public ProcessFlow processflow { get; set; }
        public Attribut attribut { get; set; }
        public Ticket ticket { get; set; }
    }
}
