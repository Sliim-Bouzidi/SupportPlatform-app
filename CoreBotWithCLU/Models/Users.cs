using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Users
    {
        public Users()
        {
            Comments = new HashSet<Comments>();
            TicketHistories = new HashSet<TicketHistories>();
            Tickets = new HashSet<Tickets>();
            UserRoles = new HashSet<UserRoles>();
        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Guid? TenantId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Passwordsalt { get; set; }

        public virtual Tenants Tenant { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<TicketHistories> TicketHistories { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
