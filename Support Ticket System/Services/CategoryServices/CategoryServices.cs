using Microsoft.EntityFrameworkCore;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly Datacontext _context;
        public CategoryServices(Datacontext context)
        {
            _context = context;
        }
        public async Task<List<TicketCategory>> AddCategorytoticket(Guid ticketid, List<string> categorynames = null)
        {
            var createdTicketCategories = new List<TicketCategory>(); // Renamed to fix typo
            if (categorynames == null)
            {
                return null;
            }
            foreach (var categoryname in categorynames)
            {
                string userInput = char.ToUpper(categoryname[0]) + categoryname.Substring(1).Trim().ToLower();

                var existingCategory = _context.Category.FirstOrDefault(c => c.Name == userInput);
                if (existingCategory == null)
                {
                    existingCategory = new Category()
                    {
                        CategoryID = Guid.NewGuid(),
                        Name = userInput,
                    };
                    _context.Category.Add(existingCategory);
                }

                var ticketCategory = new TicketCategory()
                {
                    TicketID = ticketid,
                    CategoryID = existingCategory.CategoryID,
                };
                _context.TicketCategory.Add(ticketCategory);
                createdTicketCategories.Add(ticketCategory);
            }

            await _context.SaveChangesAsync(); 
            return createdTicketCategories;
        }

        public async Task<IEnumerable<string>> GetCategories()
        {
            var categories = await _context.Category
                .Select(c=> c.Name)
                .ToListAsync();
            return categories;
        }
    }
}
