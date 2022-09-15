using Users.App.Dtos.Users;

namespace Users.App.Requests.Commands.Users
{
    public class UpdateUsersTableRequest
    {
        public List<UserRegisterDto>? UsersToRegister { get; set; }
        public List<UserUpdateDto>? UsersToUpdate { get; set; }
        public List<UserReadDto>? UsersToRemove { get; set; }
    }
}
