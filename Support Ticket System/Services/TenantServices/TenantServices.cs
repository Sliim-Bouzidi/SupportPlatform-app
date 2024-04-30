using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.TenantServices
{
    public class TenantServices : ITenantServices
    {
        private readonly Datacontext _context;
        public TenantServices(Datacontext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetAllTenant()
        {
            return await _context.tenants
                                  .Select(t => new Tenant
                                  {
                                      TenantID = t.TenantID,
                                      Name = t.Name,
                                      
                                  })
                                  .ToListAsync();
        }
        public async Task<bool> ValidateUserInTenant(string tenantName , Guid userid)
        {
            var validateUser = await _context.users
                                            .Where(u => u.UserID == userid)
                                            .Include(u => u.tenant)
                                            .FirstOrDefaultAsync(u => u.tenant.Name == tenantName);
            if (validateUser == null)
            {
                return false;
            }
            return true;
        }

    }
}
