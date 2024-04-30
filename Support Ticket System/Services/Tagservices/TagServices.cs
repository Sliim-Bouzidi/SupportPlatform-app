
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.Tagservices
{
    public class TagServices : ITagServices
    {
        private readonly Datacontext _context;
        public TagServices(Datacontext context)
        {
            _context = context;
        }
        public List<taggableitem> AddTagtoticket (Guid ticketid , List<string> tagnames = null )
        {
           var createdTaggableItems = new List<taggableitem>();
            if (tagnames== null)
            {
                return null; 
            }
            foreach (var tagname in tagnames)
            {
                string userinput = char.ToUpper(tagname[0]) + tagname.Substring(1).Trim().ToLower();


                var existingTag = _context.tags.FirstOrDefault(t => t.TagName == userinput);
                if (existingTag == null)
                {
                    existingTag = new Tag()
                    {
                        TagID = Guid.NewGuid(),
                        TagName = userinput,
                    };
                    _context.tags.Add(existingTag);
                    _context.SaveChanges();
                }
                var taggableitems = new taggableitem()
                {
                    TicketID = ticketid,
                    TagID = existingTag.TagID,
                    
                };
                _context.taggableitems.Add(taggableitems);
                _context.SaveChanges();
                createdTaggableItems.Add(taggableitems);
            }
            return createdTaggableItems;
        }
    }
}
