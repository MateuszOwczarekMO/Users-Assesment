using AutoMapper;
using MediatR;
using Users.Application.Requests.Users;
using Users.Application.Responses.Commands.Users;
using Users.Application.Validators.Users;
using Users.Domain.Models;
using Users.Repositories.Users;
using Users.Services.GenerateAgeService;

namespace Users.Application.Commands.Users
{
    public record UpdateUsersTableCommand(UpdateUsersTableRequest Request) : IRequest<UpdateUsersTableCommandResponse>;

    public class UpdateUsersTableCommandHandler : IRequestHandler<UpdateUsersTableCommand, UpdateUsersTableCommandResponse>
    {
        private IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IGenerateAgeFromDOB _generateAgeFromDOB;
        private IUpdateUsersTableRequestValidator _updateUsersTableRequestValidator;

        public UpdateUsersTableCommandHandler(IMapper mapper, IUserRepository repository, IGenerateAgeFromDOB generateAgeFromDOB, IUpdateUsersTableRequestValidator updateUsersTableRequestValidator)
        {
            _mapper = mapper;
            _repository = repository;
            _generateAgeFromDOB = generateAgeFromDOB;
            _updateUsersTableRequestValidator = updateUsersTableRequestValidator;
        }

        public async Task<UpdateUsersTableCommandResponse> Handle(UpdateUsersTableCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdateUsersTableCommandResponse();
            var validationResult = await _updateUsersTableRequestValidator.Validate(command.Request);

            if (validationResult.IsValid)
            {
                if (command.Request.UsersToRegister != null)
                {
                    if(command.Request.UsersToRegister.Count > 0)
                    {
                        var usersToRegister = _mapper.Map<List<User>>(command.Request.UsersToRegister);
                        usersToRegister.Select(c => { c.Age = _generateAgeFromDOB.GenerateAge(c.DateOfBirth); return c; }).ToList();
                        var registeringResult = await _repository.RegisterRangeOfUsersAsync(usersToRegister);

                        if (!registeringResult)
                        {
                            response.Message = "Error saving changes! Try again or contact website administrator!";
                            response.Success = false;
                            return response;
                        }
                    }
                }

                if(command.Request.UsersToUpdate != null)
                {
                    if(command.Request.UsersToUpdate.Count > 0)
                    {
                        var usersToUpdate = _mapper.Map<List<User>>(command.Request.UsersToUpdate);
                        usersToUpdate.Select(c => { c.Age = _generateAgeFromDOB.GenerateAge(c.DateOfBirth); return c; }).ToList();

                        var updatingResult = await _repository.UpdateRangeOfUsersAsync(usersToUpdate);

                        if (!updatingResult)
                        {
                            response.Message = "Error saving changes! Try again or contact website administrator!";
                            response.Success = false;
                            return response;
                        }
                    }
                }

                if(command.Request.UsersToRemove != null)
                {
                    if(command.Request.UsersToRemove.Count > 0)
                    {
                        var usersToRemove = _mapper.Map<List<User>>(command.Request.UsersToRemove);
                        var removingResult = await _repository.RemoveRangeOfUsersAsync(usersToRemove);

                        if (!removingResult)
                        {
                            response.Message = "Error saving changes! Try again or contact website administrator!";
                            response.Success = false;
                            return response;
                        }
                    }
                }

                try
                {
                    var r = await _repository.SaveChangesAsync();
                    response.Message = "Success saving changes!";
                }
                catch (Exception)
                {
                    response.Message = "Error saving changes! Try again or contact website administrator!";
                    response.Success = false;
                }
            }
            else
            {
                response.UsersToRegisterErrors = validationResult.UsersToRegisterErrors;
                response.UsersToUpdateErrors = validationResult.UsersToUpdateErrors;
                response.UsersToRemoveErrors = validationResult.UsersToRemoveErrors;
                response.Success = false;
                response.Message = "Changes not saved, check errors";
            }

            return response;
        }
    }
}
