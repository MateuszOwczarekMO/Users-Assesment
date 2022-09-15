using Users.Domain.Models;

namespace Users.Repositories.Users
{
    public interface IUserRepository
    {
        ValueTask<IEnumerable<User>> GetAllUsersAsync();
        ValueTask<User?> GetUserByIdAsync(Guid id);
        ValueTask<bool> RegisterRangeOfUsersAsync(IEnumerable<User> usersToRegister);
        ValueTask<bool> UpdateRangeOfUsersAsync(IEnumerable<User> usersToUpdate);
        ValueTask<bool> RemoveRangeOfUsersAsync(IEnumerable<User> usersToRemove);
        ValueTask<bool> HasUserAsync(Guid id);
        ValueTask<bool> SaveChangesAsync();
    }
}
