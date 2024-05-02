using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddNewStatus(string StatusName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var statusAdded = _statusservice.AddNewStatus(StatusName);
            if (statusAdded == null)
            {
            return BadRequest(ModelState);
            }
            return Ok("status has been added successfully");
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteStatus (string StatusName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var statusAdded =  await _statusservice.RemoveStatus(StatusName);
            if (statusAdded == false)
            {
                return BadRequest(ModelState);
            }
            return Ok("status has been removed successfully");
        }
    }
}
