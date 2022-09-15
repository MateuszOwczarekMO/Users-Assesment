using Users.Application.Dtos.Users;

namespace Users.Application.Requests.Users
{
    public class UpdateUsersTableRequest
    {
        public List<UserRegisterDto>? UsersToRegister { get; set; }
        public List<UserUpdateDto>? UsersToUpdate { get; set; }
        public List<UserReadDto>? UsersToRemove { get; set; }
    }
}
