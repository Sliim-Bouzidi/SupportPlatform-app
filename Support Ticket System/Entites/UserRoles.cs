namespace Support_Ticket_System.Entites
{
    public class UserRoles
    {
        public Guid UserRolesID { get; set; }
        public string RoleValue { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
