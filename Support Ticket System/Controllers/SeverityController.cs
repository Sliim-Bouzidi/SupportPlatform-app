using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket_System.Entites;
using Support_Ticket_System.Services.severity_services;
using System.Runtime.InteropServices;

namespace Support_Ticket_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeverityController : Controller
    {
        private readonly IseverityServices _severityyServices;
        public SeverityController(IseverityServices severityServices)
        {
            _severityyServices = severityServices;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Severity>> GetAllSeverityNames()
        {
            {
                var Severitynames = _severityyServices.GetSeverityNames();
                return Ok(Severitynames);
            }

        }
        [HttpGet("single severity")]
            public ActionResult<IEnumerable<Severity>> getseverity(string name) 
        {
            {
                var Severitynames = _severityyServices.GetSeverityNames();
                return Ok(Severitynames);
            }

        }



    }
}
