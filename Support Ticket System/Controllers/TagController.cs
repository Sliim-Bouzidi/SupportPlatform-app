using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket_System.Services.Tagservices;

namespace Support_Ticket_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {


        private readonly ITagServices _tagServices;



        public TagController(ITagServices tagServices)
        {
            _tagServices = tagServices;
        }
        [HttpGet]
        public IActionResult GetTagNames(Guid ticketId)
        {
            var Tags = _tagServices.GetTagNames(ticketId);
            return Ok(Tags);
        }



        [HttpPut("updatetags/{ticketId}")]
        public IActionResult UpdateTagsForTicket(Guid ticketId, List<string> tagNames)
        {
            if (tagNames == null || tagNames.Count == 0)
            {
                return BadRequest("Tag names are required.");
            }

            var updatedTags = _tagServices.UpdateTagsForTicket(ticketId, tagNames);

            if (updatedTags.Count == 0)
            {
                return NotFound("Ticket not found.");
            }

            return Ok(updatedTags);
        }

    }
}
