using System.Text.Json.Serialization;

namespace Support_Ticket_System.Entites
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] Passwordsalt { get; set; }
        public string Email { get; set; }
        public Tenant? tenant { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<UserRoles> Roles { get; set; }


        public ICollection<Usertenant> UserTenants { get; set; }

    }
}
