using Microsoft.AspNetCore.Mvc;
using Support_Ticket_System.Entites;

namespace Support_Ticket_System.Services.TenantServices
{
    public interface ITenantServices
    {
        Task<IEnumerable<object>> GetAllTenant();
        Task<bool> ValidateUserInTenant(string tenantName , Guid userid);
    }
}
