using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.ProcessFlowServices
{
    public interface IProcessFlowServices
    {
        IEnumerable<string> FirstLevelProcessFlows();
        IEnumerable<string> GetChildrenOfParentProcessFlow(string parentProcessFlowName);
    }
}
