using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Tickets
    {
        public Tickets()
        {
            Attachments = new HashSet<Attachments>();
            Comments = new HashSet<Comments>();
            Reasons = new HashSet<Reasons>();
            Statushistory = new HashSet<Statushistory>();
            Taggableitems = new HashSet<Taggableitems>();
            TicketCategory = new HashSet<TicketCategory>();
            TicketHistories = new HashSet<TicketHistories>();
        }

        public Guid TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string AssignTo { get; set; }
        public Guid? UserId { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? ProcessFlowId { get; set; }
        public Guid? PriorityId { get; set; }
        public Guid? SeverityId { get; set; }
        public string Status { get; set; }
        public Guid? TicketTypeId { get; set; }

        public virtual Priorities Priority { get; set; }
        public virtual ProcessFlows ProcessFlow { get; set; }
        public virtual Severities Severity { get; set; }
        public virtual Tenants Tenant { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Attachments> Attachments { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Reasons> Reasons { get; set; }
        public virtual ICollection<Statushistory> Statushistory { get; set; }
        public virtual ICollection<Taggableitems> Taggableitems { get; set; }
        public virtual ICollection<TicketCategory> TicketCategory { get; set; }
        public virtual ICollection<TicketHistories> TicketHistories { get; set; }
    }
}
