namespace Support_Ticket_System.DTOs
{
    public class AddProcessFlowDto
    {
        public string ProcessFlowName { get; set; }
        public Guid? ParentProcessFlowId { get; set; }
        public string TenantName { get; set; }
    }
}
