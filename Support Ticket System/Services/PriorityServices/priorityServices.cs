using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.PriorityServices
{
    public class priorityServices : IPriorityServices
    {
        private readonly Datacontext _context;
        public priorityServices(Datacontext context)
        {
            _context = context;
        }
        public Priority SetPriority(string prorityName)
        {
            
                var priority = _context.priorities
                    .FirstOrDefault(p => p.PriorityName == prorityName);

                if (priority == null)
                {

                    Console.WriteLine($"Warning: Priority with name '{prorityName}' not found.");
                }

                return priority;
            
           
        }
        public IEnumerable<string> GetPriorityNames()
        {
            var prioritynames = _context.priorities
                                    .Select(s => s.PriorityName)
                                    .ToList();
            return prioritynames;
        }
    }
    
}
