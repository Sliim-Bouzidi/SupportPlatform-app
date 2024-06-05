namespace Support_Ticket_System.Entites
{
    public class Usertenant
    {


        public Guid UserTenantId { get; set; }

        public User user { get; set; }

        public Tenant tenant { get; set; }


    }
}
