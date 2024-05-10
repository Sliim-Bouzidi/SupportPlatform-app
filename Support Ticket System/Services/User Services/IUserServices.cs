using Support_Ticket_System.Entites;
using System.IdentityModel.Tokens.Jwt;

namespace Support_Ticket_System.Services.User_Services
{
    public interface IUserServices
    {
        IEnumerable<User> GetAllUsers();
        Task<User> GetUserByID(Guid id);
        Task<string> Login(string? email, string? username, string? password);
        Task<User> Register(string username, string email, string password);
        Task<IEnumerable<string>> GetUserRoles(string username);
        (bool, JwtSecurityToken) VerifyToken(string jwtToken);


        Task<IEnumerable<string>> GetRolesofSinlgeUser(Guid UserID);
        Task<User> GetSingleUser(Guid userID);

        Task<IEnumerable<string>> GetAllRoles();


        Task<List<User>> UpdateUserRoles(Guid userId, List<string> roles);

    }
}
