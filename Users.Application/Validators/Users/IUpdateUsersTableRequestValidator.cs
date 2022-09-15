using Users.Application.Requests.Users;

namespace Users.Application.Validators.Users
{
    public interface IUpdateUsersTableRequestValidator
    {
        public Task<UpdateUsersTableRequestValidatorResult> Validate(UpdateUsersTableRequest request);
    }
}
