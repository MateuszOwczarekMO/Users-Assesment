using Users.Application.Dtos.Users;

namespace Users.Application.Validators.Users
{
    public class UpdateUsersTableRequestValidatorResult
    {
        public bool IsValid { get; set; } = true;
        public List<UserRegisterErrorDto> UsersToRegisterErrors { get; set; } = new List<UserRegisterErrorDto>();
        public List<UserUpdateErrorDto> UsersToUpdateErrors { get; set; } = new List<UserUpdateErrorDto>();
        public List<UserRemoveErrorDto> UsersToRemoveErrors { get; set; } = new List<UserRemoveErrorDto>();
    }
}
