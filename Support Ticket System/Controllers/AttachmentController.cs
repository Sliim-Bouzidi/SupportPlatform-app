using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket_System.DTOs;
using Support_Ticket_System.Services.Attachment_services;

namespace Support_Ticket_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentServices _attachmentServices;
        public AttachmentController(IAttachmentServices attachmentServices)
        {
            _attachmentServices = attachmentServices;
        }
        [HttpPost]
        public async Task<IActionResult> AddAttachment(Guid TicketID, AddAttachmentDto model)
        {
            if (model.file == null || model.file.Length == 0)
            {
                return BadRequest("File is required.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await model.file.CopyToAsync(memoryStream);
                var success = await _attachmentServices.UploadAttachment(TicketID, model.file.FileName, memoryStream.ToArray(), model.file.ContentType, model.file.Length);
                if (success)
                {
                    return Ok("Attachment uploaded successfully.");
                }
                else
                {
                    return BadRequest("Failed to upload attachment.");
                }
            }
        }
    }
}
