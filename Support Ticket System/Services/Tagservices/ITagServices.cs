using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.Tagservices
{
    public interface ITagServices
    {
        List<taggableitem> AddTagtoticket(Guid ticketid, List<string> tagnames = null);
    }
}
