using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket_System.DTOs;
using Support_Ticket_System.Services.Commentservices;
using System.Security.Claims;
using System.Security.Policy;

namespace Support_Ticket_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentServices _commentServices;
        public CommentController(ICommentServices commentservices)
        {
            _commentServices = commentservices;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(Guid ticketID, AddCommentDto request)
        {
            try
            {
                var userClaims = HttpContext.User.Identity as ClaimsIdentity;

                var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    var comment = await _commentServices.AddComment(ticketID, userId, request.text);
                    
                    return Ok(comment);
                }
                else
                {
                    return BadRequest("no comment has been added");
                }
            }
            catch (Exception)
            {
                return Unauthorized();
            }

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllComments (Guid TicketID)
        {
            var comments = await _commentServices.GetAllComments(TicketID);
            return Ok(comments);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateComment(Guid CommentID, UpdateComment request)
        {
            try
            {
                var userClaims = HttpContext.User.Identity as ClaimsIdentity;

                var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))

                {
                    var comment = await _commentServices.UpdateComment(CommentID, userId, request.Text);
                    return Ok(comment);
                }
                return BadRequest("comment was not updated");
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Guid ticketID, Guid CommentID)
        {
            var result = await _commentServices.RemoveComment(ticketID, CommentID);
            return Ok(result);
        }
    }
}
