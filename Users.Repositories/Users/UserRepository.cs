using Microsoft.EntityFrameworkCore;
using Users.Infrastucture.Storeage;
using Users.Domain.Models;

namespace Users.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _context;

        public UserRepository(UsersDbContext context)
        {
            _context = context;
        }

        public async ValueTask<bool> UpdateRangeOfUsersAsync(IEnumerable<User> usersToUpdate)
        {
            try
            {
                _context.UpdateRange(usersToUpdate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async ValueTask<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                var result = await _context.Users.ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        
        }

        public async ValueTask<bool> RegisterRangeOfUsersAsync(IEnumerable<User> usersToRegister)
        {

            try
            {
                await _context.Users.AddRangeAsync(usersToRegister);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async ValueTask<bool> RemoveRangeOfUsersAsync(IEnumerable<User> usersToRemove)
        {
            try
            {
                _context.Users.RemoveRange(usersToRemove);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async ValueTask<bool> HasUserAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async ValueTask<User?> GetUserByIdAsync(Guid id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return result;
        }

        public async ValueTask<bool> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
