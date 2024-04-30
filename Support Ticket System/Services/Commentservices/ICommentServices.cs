using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.Commentservices
{
    public interface ICommentServices
    {
        Task<Comment> AddComment(Guid ticketID, Guid userID, string text);
        Task<string> RemoveComment(Guid ticketID, Guid CommentID);
        Task<IEnumerable<Comment>> GetAllComments(Guid TicketID);
    }
}
