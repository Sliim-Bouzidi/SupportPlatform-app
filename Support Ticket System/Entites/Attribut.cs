namespace Support_Ticket_System.Entites
{
    public class Attribut
    {
        public Guid AttributID { get; set; }
        public string Name { get; set; }
        public ICollection<ProcessFlowAttrribut> PFAttributs { get; set; }
    }
}
