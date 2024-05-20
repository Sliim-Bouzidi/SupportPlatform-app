using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Statuses
    {
        public Statuses()
        {
            Statushistory = new HashSet<Statushistory>();
        }

        public Guid StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Statushistory> Statushistory { get; set; }
    }
}
