using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.DTOs;
using Support_Ticket_System.Entites;
using Support_Ticket_System.Services.User_Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Support_Ticket_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControllers : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly ILogger<UserControllers>  _logger;
        public UserControllers(IUserServices userservices , ILogger<UserControllers> logger)
        {
            _userServices = userservices;
            _logger = logger;
        }
        [HttpGet("GetAllUsers")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            {
                var Users = _userServices.GetAllUsers();
                return Ok(Users);
            }

        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(AddUserRequest request)
        {
            try
            {
                string username = request.username;
                string email = request.email;
                string password = request.password;

               
                var userRegistration = await _userServices.Register(username, email, password);

                return Ok("User is registered");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error occurred during user registration");


                return BadRequest("Failed to register user: An unexpected error occurred");
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> login(AddUserRequest request)
        {
            try
            {
                string username = request.username;
                string email = request.email;
                string password = request.password;


                var userRegistration = await _userServices.Login(email,username,password);
               

                return Ok(userRegistration);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error occurred during user login");


                return BadRequest("Failed to login user: An unexpected error occurred");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("UserRolesbyusername")]
        public async Task<ActionResult<IEnumerable<string>>> UserRoles()
        {
            try
            {
                var userClaims = HttpContext.User.Identity as ClaimsIdentity;

                var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

                if (userIdClaim != null )
                {
                    var user = await _userServices.GetUserRoles(userIdClaim);
                    return Ok(user);
                }
                else
                {
                    return BadRequest("User not found");
                }
            }
            catch (Exception)
            {
                return Unauthorized();
            }

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAuthenticatedUser() 
            {
            try
            {
                var userClaims = HttpContext.User.Identity as ClaimsIdentity;

                var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    var user = await _userServices.GetUserByID(userId);
                    return Ok(user);
                }
                else
                {
                    return BadRequest("User not found");
                }
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        



    }
}
