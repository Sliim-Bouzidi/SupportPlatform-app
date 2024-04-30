using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket_System.Entites;
using Support_Ticket_System.Services.PriorityServices;

namespace Support_Ticket_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorirtyController : Controller
    {
        private readonly IPriorityServices _PriorityService;
        public PriorirtyController(IPriorityServices priorityServices)
        {
            _PriorityService = priorityServices;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Priority>> GetAllPriorityNames()
        {
            {
                var PriorityNmaes = _PriorityService.GetPriorityNames();
                return Ok(PriorityNmaes);
            }

        }

    }
}
