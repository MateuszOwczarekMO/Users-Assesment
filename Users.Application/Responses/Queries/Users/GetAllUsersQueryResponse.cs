using Users.Application.Dtos.Users;

namespace Users.Application.Responses.Queries.Users
{
    public class GetAllUsersQueryResponse : BaseQueryResponse
    {
        public IEnumerable<UserReadDto> Users { get; set; }
    }
}
