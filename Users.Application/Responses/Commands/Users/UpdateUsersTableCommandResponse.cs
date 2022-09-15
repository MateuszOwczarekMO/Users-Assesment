using Users.Application.Dtos.Users;

namespace Users.Application.Responses.Commands.Users
{
    public class UpdateUsersTableCommandResponse : BaseCommandResponse
    {
        public List<UserRegisterErrorDto> UsersToRegisterErrors { get; set; } = new List<UserRegisterErrorDto>();
        public List<UserUpdateErrorDto> UsersToUpdateErrors { get; set; } = new List<UserUpdateErrorDto>();
        public List<UserRemoveErrorDto> UsersToRemoveErrors { get; set; } = new List<UserRemoveErrorDto>();
    }
}
