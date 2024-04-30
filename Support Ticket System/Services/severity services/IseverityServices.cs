using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.severity_services
{
    public interface IseverityServices
    {
        Severity SetSeverity(String SeverityName);
        IEnumerable<string> GetSeverityNames();
    }
}
