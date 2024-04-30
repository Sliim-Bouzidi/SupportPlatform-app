using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.PriorityServices
{
    public interface IPriorityServices
    {
        Priority SetPriority(string prorityName);
        IEnumerable<string> GetPriorityNames();
    }
}
