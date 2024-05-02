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

        public  async Task<ProcessFlow> AddProcessFlow(string ProcessFlowName, Guid? ParentProcessFlowId, string tenantname)
        {
           var tenant = _Context.tenants.Where(t=>t.Name == tenantname).FirstOrDefault();
            var processflow = new ProcessFlow
            {
                ProcessFlowName = ProcessFlowName,
                ParentProcessFlowId = ParentProcessFlowId,
                tenant = tenant 
            };
            await _Context.processFlows.AddAsync(processflow);
            await _Context.SaveChangesAsync();
            return processflow;
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

        public async Task<bool> RemoveProcessFlow(string ProcessFlowName)
        {
            var processFlow =  _Context.processFlows.Where(p=>p.ProcessFlowName == ProcessFlowName).FirstOrDefault();
            if (processFlow == null)
            {
                return false;
            }
            _Context.processFlows.Remove(processFlow);
            await _Context.SaveChangesAsync();
            return true;


        }
    }
}
