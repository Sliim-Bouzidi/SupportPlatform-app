using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Support_Ticket_System.Services.TenantServices;
using System.Security.Claims;

namespace Support_Ticket_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantServices _tenantServices;
        public TenantController(ITenantServices tenantServices)
        {
            _tenantServices = tenantServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTenants()
        {

            var tenantlist = await _tenantServices.GetAllTenant();
           
            return Ok(tenantlist);

        }
        [Authorize]
        [HttpGet("ValidateUserByTenant")]
        public async Task<IActionResult> ValidateUserByTenant(string tenantname )
        {
            try
            {
                var userClaims = HttpContext.User.Identity as ClaimsIdentity;

                var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    var validateduser = await _tenantServices.ValidateUserInTenant(tenantname, userId);
                    if (validateduser == true)
                    {
                        return Ok("User is authorized to access this tenant");
                    }
                    return StatusCode(403);
                }
                else 
                    return Unauthorized();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
            
    }
}
