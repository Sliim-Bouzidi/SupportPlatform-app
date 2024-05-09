using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.TicketTypeServices
{
    public interface ITicketTypeServices
    {
        TicketType SetType(string TypeName);
        Task<IEnumerable<string>> GetTicketTypes();
    }
}
