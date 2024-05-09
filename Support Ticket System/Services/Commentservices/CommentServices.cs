using Microsoft.EntityFrameworkCore;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;
using Support_Ticket_System.Services.ticketservices;
using System.CodeDom;
using System.Runtime.ConstrainedExecution;

namespace Support_Ticket_System.Services.Commentservices
{
    public class CommentServices : ICommentServices
    {
        private readonly Datacontext _context;
        private readonly ITicketService _ticketService;
        public CommentServices(Datacontext context, ITicketService ticketService)
        {
            _context = context;
            _ticketService = ticketService;

            
        }
        public async Task<Comment> AddComment(Guid ticketID, Guid userID, string textvalue)
        {
            var user = await _context.users.Where(u => u.UserID == userID).FirstOrDefaultAsync();
            var ticket = await _context.tickets.Where(u => u.TicketID == ticketID).FirstOrDefaultAsync();
            Console.WriteLine(ticket);

            if (user == null || ticket == null)
            {
                
                throw new InvalidOperationException("User or ticket not found.");
            }           
            var comment = new Comment
            {
                CommentID = Guid.NewGuid(),
                text = textvalue,
                Date = DateTime.Now,
                user = user,
                ticket = ticket
            };
            
            _context.comments.Add(comment);
            await _context.SaveChangesAsync();
            var changetype = "Comment added";
            await _ticketService.StoreInTicketHistory(ticketID, userID, changetype, null, comment.text);

            return comment;
        }

        public async Task<IEnumerable<Comment>> GetAllComments(Guid TicketID)
        {
            var comments = await _context.comments
                .Where(c => c.ticket.TicketID == TicketID)
                .Include(c => c.user) // Eager loading the User entity
                .Select(c => new Comment
                {
                    CommentID = c.CommentID,
                    text = c.text,
                    Date = c.Date,
                    user = c.user, // Include the user entity
                })
                .OrderByDescending(c => c.Date)
                .ToListAsync();

            return comments;
        }

        public async Task<string> RemoveComment(Guid ticketID , Guid CommentID)
        {
            var comment = await _context.comments.Where(c => c.ticket.TicketID == ticketID || c.CommentID == CommentID).FirstOrDefaultAsync();
            if (comment == null)
            {
                return "Comment not found.";
            }

            _context.comments.Remove(comment);
            await _context.SaveChangesAsync();
            return "comment has been removed";
        }
        public async Task<Comment> UpdateComment(Guid CommentID, Guid userID, string text)
        {

            var comment = await _context.comments.Where(c => c.CommentID == CommentID).Include(c => c.user.UserID).FirstOrDefaultAsync();
            if (comment.user.UserID != userID)
            {
                throw new InvalidOperationException("User is not authorized to update this comment.");
            }

            comment.text = text;
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
