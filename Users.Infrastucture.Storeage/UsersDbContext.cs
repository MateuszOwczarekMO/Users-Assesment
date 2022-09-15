using Microsoft.EntityFrameworkCore;
using Users.Domain.Models;

namespace Users.Infrastucture.Storeage
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
