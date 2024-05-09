using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.CategoryServices
{
    public interface ICategoryServices
    {
        Task<List<TicketCategory>> AddCategorytoticket(Guid ticketid, List<string> categorynames = null);
         Task<IEnumerable<string>> GetCategories();
    }
}
