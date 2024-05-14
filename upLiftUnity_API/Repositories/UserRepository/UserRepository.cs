using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;
using upLiftUnity_API.Services.EmailSender;

namespace upLiftUnity_API.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly APIDbContext _appDBContext;

       
        public UserRepository(APIDbContext context)
        {
            _appDBContext = context ??
                throw new ArgumentNullException(nameof(context));
          
          
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _appDBContext.Users.ToListAsync();
        }
        public async Task<User> GetUserById(int ID)
        {
            return await _appDBContext.Users.FindAsync(ID);
        }

        public async Task<User> UpdateUser(User objUser)
        {
            _appDBContext.Entry(objUser).State = EntityState.Modified;
            await _appDBContext.SaveChangesAsync();
            return objUser;
        }
        public bool DeleteUser(int ID)
        {
            bool result = false;
            var user = _appDBContext.Users.Find(ID);
            if (user != null)
            {
                _appDBContext.Entry(user).State = EntityState.Deleted;
                _appDBContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }


        public async Task<IEnumerable<User>> GetUsersByRoleId(int roleId)
        {
            return await _appDBContext.Users.Where(user => user.RoleId == roleId).ToListAsync();
        }
        public string GetUserRole(int roleId)
        {
            var roleName = _appDBContext.Roles.SingleOrDefault(r => r.Id == roleId).RoleDesc;
            return roleName;
        }

        public async Task<bool> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = _appDBContext.Users.FirstOrDefault(u => u.Id == userId);
   
            if (user == null || !BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
            {
                Console.WriteLine("Old password does not match.");
                return false;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            await _appDBContext.SaveChangesAsync();

            return true;
        }


    }
}