using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket_System.Entites;
using Support_Ticket_System.Services.TicketTypeServices;

namespace Support_Ticket_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketTypeController : ControllerBase
    {
        private readonly ITicketTypeServices _ticketTypeServices;
        public TicketTypeController(ITicketTypeServices ticketTypeServices)
        {
            _ticketTypeServices = ticketTypeServices;
        }
        [HttpGet]
        public async Task<IEnumerable<string>> GetTicketTypes()
        {
            var tickettypes =  await _ticketTypeServices.GetTicketTypes();
            return tickettypes;
        }
    }
}
