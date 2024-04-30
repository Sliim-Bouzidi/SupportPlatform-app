using Microsoft.EntityFrameworkCore;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.ProcessFlowServices
{
    public class ProcessFlowServices : IProcessFlowServices
    {
        private readonly Datacontext _Context;
        public ProcessFlowServices(Datacontext context)
        {
            _Context = context;
        }
        public IEnumerable<string> FirstLevelProcessFlows()
        {

            var firstLevelProcessFlows = _Context.processFlows
                                        .Where(pf => pf.ParentProcessFlowId == null)
                                        .Select(pf => pf.ProcessFlowName)
                                        .ToList();

            return firstLevelProcessFlows;
        }
        public IEnumerable<string> GetChildrenOfParentProcessFlow(string parentProcessFlowName)
        {
            var parentProcessFlow = _Context.processFlows
                .FirstOrDefault(pf => pf.ProcessFlowName == parentProcessFlowName);

            if (parentProcessFlow == null)
            {
                return Enumerable.Empty<string>(); 
            }

            var childrenNames = _Context.processFlows
                .Where(pf => pf.ParentProcessFlowId == parentProcessFlow.ProcessFlowId)
                .Select(pf => pf.ProcessFlowName) 
                .ToList();

            return childrenNames; 
        }

    }
}
