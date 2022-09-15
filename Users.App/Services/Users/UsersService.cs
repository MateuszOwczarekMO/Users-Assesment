using Users.App.Requests.Commands.Users;
using Users.App.Responses.Commands;
using Users.App.Responses.Queries;

namespace Users.App.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly static string _baseUrl = "api/users";

        public UsersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetAllUsersQueryResponse> GetAllUsers()
        {
            return await _httpClient.GetFromJsonAsync<GetAllUsersQueryResponse>(_baseUrl);
        }

        public async Task<UpdateUsersTableCommandResponse> UpdateUsersTable(UpdateUsersTableRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, request);
            var returnValue = response.Content.ReadFromJsonAsync<UpdateUsersTableCommandResponse>().Result;
            return returnValue;
        }
    }
}
