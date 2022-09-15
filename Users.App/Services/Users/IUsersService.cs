using Users.App.Requests.Commands.Users;
using Users.App.Responses.Commands;
using Users.App.Responses.Queries;

namespace Users.App.Services.Users
{
    public interface IUsersService
    {
        Task<GetAllUsersQueryResponse> GetAllUsers();
        Task<UpdateUsersTableCommandResponse> UpdateUsersTable(UpdateUsersTableRequest request);
    }
}
