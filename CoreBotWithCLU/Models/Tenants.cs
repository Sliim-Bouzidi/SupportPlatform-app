using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Tenants
    {
        public Tenants()
        {
            ProcessFlows = new HashSet<ProcessFlows>();
            Tickets = new HashSet<Tickets>();
            Users = new HashSet<Users>();
        }

        public Guid TenantId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProcessFlows> ProcessFlows { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
