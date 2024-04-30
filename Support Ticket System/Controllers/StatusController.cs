using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket_System.Entites;
using Support_Ticket_System.Services.ProcessFlowServices;
using Support_Ticket_System.Services.status_services;

namespace Support_Ticket_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IStatusservice _statusservice;
        public StatusController(IStatusservice statusservice)
        {
            _statusservice = statusservice;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Status>> GetallStatusNames()
        {
            var firstLevelProcessFlows = _statusservice.GetStatusNames();
            return Ok(firstLevelProcessFlows);
        }

        [HttpGet("statusHistory")]
        public async Task<IActionResult> getStatusHistory(Guid TicketID)
        {
            var statusHistory = await _statusservice.GetStatusHistoryOfTicket(TicketID);
            return Ok(statusHistory);
        }
    }
}
