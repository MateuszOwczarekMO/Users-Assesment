using Users.App.Dtos.Users;

namespace Users.App.Responses.Commands
{
    public class UpdateUsersTableCommandResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<UserRegisterErrorDto> UsersToRegisterErrors { get; set; } = new List<UserRegisterErrorDto>();
        public List<UserUpdateErrorDto> UsersToUpdateErrors { get; set; } = new List<UserUpdateErrorDto>();
        public List<UserRemoveErrorDto> UsersToRemoveErrors { get; set; } = new List<UserRemoveErrorDto>();
    }
}
