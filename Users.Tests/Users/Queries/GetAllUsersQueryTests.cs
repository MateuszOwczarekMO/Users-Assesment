using AutoMapper;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Dtos.Users;
using Users.Application.Profiles.Users;
using Users.Application.Queries.Users;
using Users.Repositories.Users;
using Users.Tests.Mocks;
using Xunit;

namespace Users.Tests.Users.Queries
{
    public class GetAllUsersQueryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUow;
        private readonly GetAllUsersQueryHandler _handler;

        public GetAllUsersQueryTests()
        {
            _mockUow = MockUserRepository.GetUsersRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<UsersProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetAllUsersQueryHandler(_mapper, _mockUow.Object);
        }

        [Fact]
        public async Task Users_Get_Query_Successfull()
        {
            var response = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);
            response.Users.ShouldBeOfType<List<UserReadDto>>();
            response.Users.Count().ShouldBe(2);
        }
    }
}
