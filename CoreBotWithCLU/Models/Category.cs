using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Category
    {
        public Category()
        {
            TicketCategory = new HashSet<TicketCategory>();
        }

        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TicketCategory> TicketCategory { get; set; }
    }
}
