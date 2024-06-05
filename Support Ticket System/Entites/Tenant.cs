namespace Support_Ticket_System.Entites
{
    public class Tenant
    {
        public Guid TenantID { get; set; }
        public string Name { get; set; }
        public ICollection<User> users{ get; set; }
        public ICollection<Ticket> tickets { get; set; }
        public ICollection<ProcessFlow> processflows { get; set; }


        public ICollection<Usertenant> UserTenants { get; set; }
    }
}
