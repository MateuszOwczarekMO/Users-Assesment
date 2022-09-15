using Users.App.Dtos.Users;

namespace Users.App.Responses.Queries
{
    public class GetAllUsersQueryResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public IEnumerable<UserReadDto> Users { get; set; }
    }
}
