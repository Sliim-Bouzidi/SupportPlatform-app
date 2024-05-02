using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.ProcessFlowServices
{
    public interface IProcessFlowServices
    {
        IEnumerable<string> FirstLevelProcessFlows();
        IEnumerable<string> GetChildrenOfParentProcessFlow(string parentProcessFlowName);
        Task<ProcessFlow> AddProcessFlow(string ProcessFlowName , Guid? ParentProcessFlowId , string tenantname );
        Task<bool> RemoveProcessFlow(string ProcessFlowName );
    }
}
