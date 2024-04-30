using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.severity_services
{
    public class SeverityServices : IseverityServices
    {
        private readonly Datacontext _context;
        public SeverityServices(Datacontext context)
        {
            _context = context;
            
        }
        public Severity SetSeverity(string SeverityName)
        {
            var severity = _context.severities.Where(s => s.SeverityName == SeverityName).FirstOrDefault(); ;
            return severity;
        }
        public IEnumerable<string> GetSeverityNames()
        {
            var SeverityNames = _context.severities
                                    .Select(s => s.SeverityName)
                                    .ToList();
            return SeverityNames;
        }


    }
}
