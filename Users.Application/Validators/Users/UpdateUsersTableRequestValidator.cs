using Users.Application.Dtos.Users;
using Users.Application.Requests.Users;
using Users.Repositories.Users;

namespace Users.Application.Validators.Users
{
    public class UpdateUsersTableRequestValidator : IUpdateUsersTableRequestValidator
    {
        private readonly IUserRepository _repository;

        public UpdateUsersTableRequestValidator(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateUsersTableRequestValidatorResult> Validate(UpdateUsersTableRequest request)
        {
            var result = new UpdateUsersTableRequestValidatorResult();

            if (request.UsersToRegister != null)
            {
                var validator = new UserRegisterDtoValidator();

                foreach (var userToRegister in request.UsersToRegister)
                {
                    var validationResult = validator.Validate(userToRegister);

                    if (!validationResult.IsValid)
                    {
                        result.UsersToRegisterErrors.Add(new UserRegisterErrorDto
                        {
                            Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList(),
                            UserRegisterDto = userToRegister
                        });
                    }
                }
            }

            if (request.UsersToUpdate != null)
            {
                var validator = new UserUpdateDtoValidator();

                foreach (var userToUpdate in request.UsersToUpdate)
                {
                    var validationResult = validator.Validate(userToUpdate);

                    if (!validationResult.IsValid)
                    {
                        result.UsersToUpdateErrors.Add(new UserUpdateErrorDto
                        {
                            Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList(),
                            UserUpdateDto = userToUpdate
                        });
                    }
                }
            }

            if (request.UsersToRemove != null)
            {
                foreach (var userToRemove in request.UsersToRemove)
                {
                    var ifExists = await _repository.HasUserAsync(userToRemove.Id);
                    if (!ifExists)
                    {
                        result.UsersToRemoveErrors.Add(new UserRemoveErrorDto
                        {
                            Errors = new List<string> { $"User with this id: {userToRemove.Id} does not exists!" },
                            UserToRemove = userToRemove
                        });
                    }
                }
            }

            if (result.UsersToRegisterErrors.Count != 0 || result.UsersToUpdateErrors.Count != 0 || result.UsersToRemoveErrors.Count != 0)
                result.IsValid = false;

            return result;
        }
    }
}
