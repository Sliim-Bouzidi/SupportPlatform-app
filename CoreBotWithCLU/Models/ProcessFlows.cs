using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class ProcessFlows
    {
        public ProcessFlows()
        {
            InverseParentProcessFlow = new HashSet<ProcessFlows>();
            Tickets = new HashSet<Tickets>();
        }

        public Guid ProcessFlowId { get; set; }
        public string ProcessFlowName { get; set; }
        public Guid? ParentProcessFlowId { get; set; }
        public Guid TenantId { get; set; }

        public virtual ProcessFlows ParentProcessFlow { get; set; }
        public virtual Tenants Tenant { get; set; }
        public virtual ICollection<ProcessFlows> InverseParentProcessFlow { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
    }
}
