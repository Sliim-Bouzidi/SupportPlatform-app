using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.DTOs;
using Support_Ticket_System.Entites;
using Support_Ticket_System.Services.status_services;
using Support_Ticket_System.Services.ticketservices;
using Support_Ticket_System.Services.User_Services;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Support_Ticket_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]


    public class ticketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IUserServices _userServices;
        private readonly ILogger<ticketController> _logger;




        public ticketController(ITicketService ticketService, IUserServices userservices, ILogger<ticketController> logger)
        {

            _ticketService = ticketService;
            _userServices = userservices;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketDTO request)
        {
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            string title = request.title;
            string description = request.description;
            string assignTo = request.assignTo;
            string statusName = request.statusName;
            string processflow = request.processflowName;
            string user = request.username;
            string tenantname = request.tenantname;
            string severityname = request.SeverityName;
            string priorityname = request.PriorirtyName;
            List<string> Tags = request.Tags;



            Ticket ticket = await _ticketService.CreateTicket(title, description, assignTo, statusName, processflow, user, tenantname, priorityname, severityname, Tags);
            return Ok("ticket created succesfully");
        }


        [HttpGet]

        public async Task<IActionResult> GetAllTicketsbytenant(string TenantName)
        {


            var tickets = await _ticketService.GetAllTicketsAsync(TenantName);
            return Ok(tickets);

        }
        [HttpGet("TicketDetails")]
        public async Task<IActionResult> GetTicketDetails(Guid ticketID)
        {
            var ticketdetails = await _ticketService.ticketDetails(ticketID);
            if (ticketdetails == null)
            {
                return BadRequest("Ticket does not exist");
            }
            return Ok(ticketdetails);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTicket(UpdateTicketDto request, Guid TicketID)
        {
            try
            {
                var userClaims = HttpContext.User.Identity as ClaimsIdentity;

                var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))

                {

                    Ticket ticket = await _ticketService.UpdateTicket(TicketID, userId, request.title, request.description, request.assignTo, request.statusName, request.Tags);
                    return Ok("ticket updated successfully");
                }
                else
                {
                    return BadRequest("User not found");
                }
            }
            catch (InvalidOperationException ex)
            {

                _logger.LogError(ex, "Invalid operation occurred while updating the ticket.");


                return BadRequest("Invalid operation occurred while updating the ticket.");
            }



        }
        [HttpGet("TicketNotes")]
        public async Task<IActionResult> GetTicketHistory(Guid TicketID)
        {
            var ticketHistory = await _ticketService.GetTicketHistoryMessages(TicketID);
            if (ticketHistory == null)
            {
                return BadRequest("there are no ticket notes");
            }
            return Ok(ticketHistory);
        }
    }

}
