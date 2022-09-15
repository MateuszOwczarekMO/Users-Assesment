using AutoMapper;
using MediatR;
using Users.Application.Dtos.Users;
using Users.Application.Responses.Queries.Users;
using Users.Repositories.Users;

namespace Users.Application.Queries.Users
{
    public record GetAllUsersQuery : IRequest<GetAllUsersQueryResponse>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersQueryResponse>
    {
        private IMapper _mapper;
        private readonly IUserRepository _repository;

        public GetAllUsersQueryHandler(IMapper mapper,IUserRepository repository)
        {
            _mapper = mapper;   
            _repository = repository;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var response = new GetAllUsersQueryResponse();
            try
            {
                var result = await _repository.GetAllUsersAsync();
                var users = _mapper.Map<IEnumerable<UserReadDto>>(result);
                response.Users = users;
                return response;
            }
            catch (Exception)
            {
                response.Message = "Error geting data";
                response.Success = false;
                return response;
            }
        }
    }
}
