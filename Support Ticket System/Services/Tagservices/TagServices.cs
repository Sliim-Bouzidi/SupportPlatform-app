
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




        public List<string> GetTagNames(Guid ticketid)
        {
            var ticket = _context.tickets.Where(t => t.TicketID == ticketid).FirstOrDefault();
            var tags = _context.taggableitems.Where(t => t.TicketID == ticket.TicketID)
                .Select(t => t.tag.TagName).ToList();
            return tags;
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
                    tagName = tagname

                    
                };
                _context.taggableitems.Add(taggableitems);
                _context.SaveChanges();
                createdTaggableItems.Add(taggableitems);
            }
            return createdTaggableItems;
        }



                public List<taggableitem> UpdateTagsForTicket(Guid ticketId, List<string> tagNames)
        {
            var updatedTaggableItems = new List<taggableitem>();
            var existingTags = _context.tags.ToList();
 
            foreach (var tagName in tagNames)
            {
                string userinput = char.ToUpper(tagName[0]) + tagName.Substring(1).Trim().ToLower();
 
                var existingTag = existingTags.FirstOrDefault(t => t.TagName == userinput);
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
 
                var existingTaggableItem = _context.taggableitems.FirstOrDefault(t => t.TicketID == ticketId && t.TagID == existingTag.TagID);
                if (existingTaggableItem == null)
                {
                    var newTaggableItem = new taggableitem()
                    {
                        TicketID = ticketId,
                        TagID = existingTag.TagID,
                        tagName = tagName,
                    };
                    _context.taggableitems.Add(newTaggableItem);
                    _context.SaveChanges();
                    updatedTaggableItems.Add(newTaggableItem);
                }
            }
 
            return updatedTaggableItems;
        }
            }
}
