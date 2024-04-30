namespace Support_Ticket_System.Entites
{
    public class ProcessFlow
    {
        public Guid ProcessFlowId { get; set; }
        public string ProcessFlowName { get; set; }
        public Guid? ParentProcessFlowId { get; set; }
        public ProcessFlow Parent { get; set; } 
        public List<ProcessFlow> Children { get; set; }
        public ICollection<Ticket> tickets { get; set; }
        public Tenant tenant { get; set; }
    }
}
