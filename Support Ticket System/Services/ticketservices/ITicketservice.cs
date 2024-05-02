using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.ticketservices
{
    public interface ITicketService
    {
       Task<Ticket> CreateTicket(string title, string description, string assignTo, string statusName,string processFlowId, string userId ,string tenantname , string priority , string severity ,List<string> tag);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync(string TenantName);
        Task<bool> StoreInTicketHistory(Guid ticketID, Guid? userID, string changetype, string oldvalue = null, string newvalue = null);
        Task<Ticket> UpdateTicket(Guid TicketID, Guid UserID, string title, string description, string assignTo, string statusName, List<string> tag);
        Task<Ticket> ticketDetails(Guid TicketID);
        Task<IEnumerable<string>> GetTicketHistoryMessages(Guid TicketID);
    }

}