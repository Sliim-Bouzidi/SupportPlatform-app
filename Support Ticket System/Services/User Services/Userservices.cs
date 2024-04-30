using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Support_Ticket_System.DataContext;
using Support_Ticket_System.Entites;
using Support_Ticket_System.Services.User_Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Support_Ticket_System.Services
{
    public class Userservices : IUserServices
    {
        private readonly Datacontext _context;
        private readonly IConfiguration _configuration;
        public Userservices(Datacontext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; 
        }
        public IEnumerable<User> GetAllUsers()
        {
            var Users = _context.users
                .Include(x=>x.Roles)
                .Select(u => new User
                {
                    Username = u.Username, Email = u.Email, Roles= u.Roles
                   

                })
                .ToList();
            return Users;

        }
       public async Task<User> GetUserByID(Guid id)
        {
            return await _context.users.Where(u=>u.UserID == id)
                
                .FirstOrDefaultAsync();
        }
        public async Task<User> Register(string username,string email, string password )
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                PasswordHash = passwordHash,
                Passwordsalt = passwordSalt,
                Username = username,
                Email = email,
               

            };
            _context.users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public async Task<string> Login (string? email , string? username , string? password)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.Username == username || u.Email == email);
            if (user == null )
            {
                return "User was not found";
            }
            if (!VerifyPasswordHash(password , user.PasswordHash , user.Passwordsalt))
            {
                return "wrong password";
            }
            string token = CreateToken(user);
            
            return (token);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] storedPasswordHash, byte[] storedPasswordSalt)
        {
            using (var hmac = new HMACSHA512(storedPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));


                for (int i = 0; i < storedPasswordHash.Length; i++)
                {
                    if (computedHash[i] != storedPasswordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        private string CreateToken(User user)
        {
            

            var userrole = _context.users
                   .Include(u => u.Roles)
                   .FirstOrDefault(u => u.Username == user.Username);
            var userId = userrole.UserID.ToString();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, userId)
                
                
                
            };
            foreach (var UserRole in userrole.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,UserRole.RoleValue));
            }
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection( "AppSettings:Token").Value ?? null ));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);


            return jwt;
        }
        public (bool, JwtSecurityToken) VerifyToken(string jwtToken)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value ?? null));

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false, 
                    ValidateAudience = false,
                    
                };

                
                tokenHandler.ValidateToken(jwtToken, validationParameters, out var validatedToken);


                return (true, (JwtSecurityToken)validatedToken);
            }
            catch (SecurityTokenValidationException)
            {

                return (false, null);
                
            }
        }
        public async Task<IEnumerable<string>> GetUserRoles(string username)
        {
            var roles = await _context.UserRoles
                                      .Where(ur => ur.User.Username == username)
                                      .Select(ur => ur.Role.RoleName)
                                      .ToListAsync();

            return roles;
        }



    }
}
