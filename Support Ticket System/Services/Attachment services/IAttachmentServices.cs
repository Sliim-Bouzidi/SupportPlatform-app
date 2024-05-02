namespace Support_Ticket_System.Services.Attachment_services
{
    public interface IAttachmentServices
    {
        Task<bool> UploadAttachment(Guid ticketId, string fileName, byte[] fileData, string contentType, long fileSize);
    }
}
