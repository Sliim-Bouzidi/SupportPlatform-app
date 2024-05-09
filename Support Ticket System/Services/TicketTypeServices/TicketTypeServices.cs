using Microsoft.EntityFrameworkCore;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.TicketTypeServices
{
    public class TicketTypeServices : ITicketTypeServices
    {
        private readonly Datacontext _context;
        public TicketTypeServices(Datacontext context)
        {
            _context = context;
        }
        public TicketType SetType(string TypeName)
        {

            var type = _context.TicketType
                .FirstOrDefault(p => p.Name == TypeName);

            if (type == null)
            {

                Console.WriteLine($"Warning: Priority with name '{type}' not found.");
            }

            return type;


        }
        public async Task<IEnumerable<string>> GetTicketTypes() 
        {
            var types = await _context.TicketType
                                     .Select(s => s.Name)
                                     .ToListAsync();
            return types;
        }
    }
}
