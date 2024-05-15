using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.ticketservices
{
    public interface ITicketService
    {
       Task<Ticket> CreateTicket(string title, string description, string assignTo, string processFlowname, string username, string tenantname, string priorityname, string severityname, List<string> tag, string? DossierNumber, string? SalesOrderNumber, string? WorkingOrderNumber, string? AssistancePlanNumber, IFormFile? file);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync(string TenantName);
        Task<bool> StoreInTicketHistory(Guid ticketID, Guid? userID, string changetype, string oldvalue = null, string newvalue = null);
        Task<Ticket> UpdateTicket(Guid TicketID, Guid UserID, string title, string description, string assignTo, string statusName, List<string> tag);
        Task<Ticket> ticketDetails(Guid TicketID);
        Task<IEnumerable<string>> GetTicketHistoryMessages(Guid TicketID);

        Task<bool> RemoveTicket(Guid ticketID);
    }

}