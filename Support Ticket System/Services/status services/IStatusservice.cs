using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.status_services
{
    public interface IStatusservice
    {
        IEnumerable<Status> GetAllStatuses();
        StatusHistory SetStatus(Guid TicketID, string statusName = null);
        IEnumerable<string> GetStatusNames();
        Task<IEnumerable<StatusHistory>> GetStatusHistoryOfTicket(Guid TicketID);
    }
}
