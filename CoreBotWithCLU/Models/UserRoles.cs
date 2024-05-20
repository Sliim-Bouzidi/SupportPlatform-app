using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class UserRoles
    {
        public Guid UserRolesId { get; set; }
        public string RoleValue { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual Users User { get; set; }
    }
}
