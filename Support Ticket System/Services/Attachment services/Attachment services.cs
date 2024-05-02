using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;
using Support_Ticket_System.Services.Attachment_services;
using System.Net.Mail;
using Attachment = Support_Ticket_System.Entites.Attachment;

namespace Support_Ticket_System.Services
{
    public class Attachmentservices : IAttachmentServices
    {
        private readonly Datacontext _context;
        public Attachmentservices(Datacontext context)
        {
            _context = context;
            
        }

        public async Task<bool> UploadAttachment(Guid ticketId, string fileName, byte[] fileData, string contentType, long fileSize)
        {
            var ticket = await _context.tickets.Where(t=>t.TicketID == ticketId).FirstOrDefaultAsync();
            if (!IsFileSafe(contentType))
            {
                return false;
            }
            var attachment = new Attachment
            {
                Id = Guid.NewGuid(),
                FileName = fileName,
                FileData = fileData,
                ContentType = contentType,
                FileSize = fileSize,
                ticket = ticket
                
                
            };
            return true ;
        }
        private bool IsFileSafe(string contenttype)
        {
            
            var AllowedContentTypes = new string[] { "image/jpeg", "image/png", "application/pdf" };
            
            if (!AllowedContentTypes.Contains(contenttype))
            {
                return false;
            }

            return true;
        }

    }
}
