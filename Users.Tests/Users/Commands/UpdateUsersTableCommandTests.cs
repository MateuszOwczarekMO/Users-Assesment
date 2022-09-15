using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Commands.Users;
using Users.Application.Dtos.Users;
using Users.Application.Profiles.Users;
using Users.Application.Requests.Users;
using Users.Application.Responses.Commands.Users;
using Users.Application.Validators.Users;
using Users.Repositories.Users;
using Users.Services.DateTimeProviderService;
using Users.Services.GenerateAgeService;
using Users.Tests.Mocks;
using Xunit;

namespace Users.Tests.Users.Commands
{
    public class UpdateUsersTableCommandTests
    {
        private readonly static string _veryLongString = "verylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylongverylong";
        private readonly IMapper _mapper;
        private readonly IGenerateAgeFromDOB _generateAgeFromDOB;
        private readonly IUpdateUsersTableRequestValidator _updateUsersTableRequestValidator;
        private readonly Mock<IUserRepository> _mockUow;

        private readonly UserRegisterDto _userRegisterDto;
        private readonly UserUpdateDto _userUpdateDto;
        private readonly UserReadDto _userToRemoveDto;
        private readonly UpdateUsersTableRequest _updateUsersTableRequest;

        private readonly UpdateUsersTableCommandHandler _handler;

        public UpdateUsersTableCommandTests()
        {
            _mockUow = MockUserRepository.GetUsersRepository();
            _updateUsersTableRequestValidator = new UpdateUsersTableRequestValidator(_mockUow.Object);
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<UsersProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _generateAgeFromDOB = new GenerateAgeFromDOB(new DateTimeProvider());
            _handler = new UpdateUsersTableCommandHandler(_mapper, _mockUow.Object, _generateAgeFromDOB, _updateUsersTableRequestValidator);


            _userRegisterDto = new UserRegisterDto
            {
                TempId = Guid.NewGuid(),
                FirstName = "Pawel",
                LastName = "Bialy",
                StreetName = "Zielona",
                HouseNumber = "3B",
                ApartmentNumber = null,
                PostalCode = "39-100",
                Town = "Wroclaw",
                PhoneNumber = "06935893571",
                DateOfBirth = DateTimeOffset.Now.AddYears(-25),
            };

            _userUpdateDto = new UserUpdateDto
            {
                Id = Guid.Parse("0f59b5d2-d40b-4ad2-afb8-3e7c52b21df2"),
                FirstName = "Pawel",
                LastName = "Bialy",
                StreetName = "Zielona",
                HouseNumber = "3B",
                ApartmentNumber = null,
                PostalCode = "39-100",
                Town = "Wroclaw",
                PhoneNumber = "05935893571",
                DateOfBirth = DateTimeOffset.Now.AddYears(-25),
            };

            _userToRemoveDto = new UserReadDto
            {
                Id = Guid.Parse("0303adb9-d4f5-44ef-a25f-667c98636fa6"),
                FirstName = "Dawid",
                LastName = "Czarny",
                StreetName = "Stalowa",
                HouseNumber = "4A",
                ApartmentNumber = null,
                PostalCode = "39-100",
                Town = "Rzeszow",
                PhoneNumber = "06545653571",
                DateOfBirth = DateTimeOffset.Now.AddYears(-30),
                Age = 30
            };

            _updateUsersTableRequest = new UpdateUsersTableRequest();
        }

        #region REGISTERING_USER

        [Fact]
        public async Task Valid_User_Registered()
        {
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeTrue();
            users.Count().ShouldBe(3);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_FirstName_Empty()
        {
            _userRegisterDto.FirstName = "";
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_FirstName_Is_Invalid()
        {
            _userRegisterDto.FirstName = "432141 4 ew";
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_FirstName_TooLong()
        {
            _userRegisterDto.FirstName = _veryLongString;
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_LastName_Empty()
        {
            _userRegisterDto.LastName = "";
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_LastName_Is_Invalid()
        {
            _userRegisterDto.LastName = "432141 4 ew";
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_LastName_TooLong()
        {
            _userRegisterDto.LastName = _veryLongString;
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_StreetName_Empty()
        {
            _userRegisterDto.StreetName = "";
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_StreetName_TooLong()
        {
            _userRegisterDto.StreetName = _veryLongString;
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_HouseNumber_Empty()
        {
            _userRegisterDto.HouseNumber = "";
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_HouseNumber_Is_Invalid()
        {
            _userRegisterDto.HouseNumber = "ok 6543 6reg 34o";
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_HouseNumber_TooLong()
        {
            _userRegisterDto.HouseNumber = _veryLongString;
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_ApartmentNumber_Is_Invalid()
        {
            _userRegisterDto.ApartmentNumber = "ok 6543 6reg 34o";
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_ApartmentNumber_TooLong()
        {
            _userRegisterDto.ApartmentNumber = _veryLongString;
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_PostalCode_Empty()
        {
            _userRegisterDto.PostalCode = "";
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_PostalCode_TooLong()
        {
            _userRegisterDto.PostalCode = _veryLongString;
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_Town_Empty()
        {
            _userRegisterDto.Town = "";
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_Town_TooLong()
        {
            _userRegisterDto.Town = _veryLongString;
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_PhoneNumber_Empty()
        {
            _userRegisterDto.PhoneNumber = "";
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_PhoneNumber_Is_Invalid()
        {
            _userRegisterDto.PhoneNumber = "ok 6543 6reg .., 34o";
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_PhoneNumber_TooLong()
        {
            _userRegisterDto.PhoneNumber = _veryLongString;
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Registered_DateOfBirth_Is_Invalid_DOB_Is_After_CurrentDate()
        {
            _userRegisterDto.DateOfBirth = DateTimeOffset.Now.AddYears(1);
            _updateUsersTableRequest.UsersToRegister = new List<UserRegisterDto> { _userRegisterDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        #endregion

        #region UPDATING_USER
        [Fact]
        public async Task Valid_User_Updated()
        {
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_FirstName_Empty()
        {
            _userUpdateDto.FirstName = "";
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_FirstName_Is_Invalid()
        {
            _userUpdateDto.FirstName = "432141 4 ew";
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_FirstName_TooLong()
        {
            _userUpdateDto.FirstName = _veryLongString;
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_LastName_Empty()
        {
            _userUpdateDto.LastName = "";
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_LastName_Is_Invalid()
        {
            _userUpdateDto.LastName = "432141 4 ew";
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_LastName_TooLong()
        {
            _userUpdateDto.LastName = _veryLongString;
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_StreetName_Empty()
        {
            _userUpdateDto.StreetName = "";
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_StreetName_TooLong()
        {
            _userUpdateDto.StreetName = _veryLongString;
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_HouseNumber_Empty()
        {
            _userUpdateDto.HouseNumber = "";
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_HouseNumber_Is_Invalid()
        {
            _userUpdateDto.HouseNumber = "ok 6543 6reg 34o";
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_HouseNumber_TooLong()
        {
            _userUpdateDto.HouseNumber = _veryLongString;
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_ApartmentNumber_Is_Invalid()
        {
            _userUpdateDto.ApartmentNumber = "ok 6543 6reg 34o";
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_ApartmentNumber_TooLong()
        {
            _userUpdateDto.ApartmentNumber = _veryLongString;
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_PostalCode_Empty()
        {
            _userUpdateDto.PostalCode = "";
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_PostalCode_TooLong()
        {
            _userUpdateDto.PostalCode = _veryLongString;
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_Town_Empty()
        {
            _userUpdateDto.Town = "";
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_Town_TooLong()
        {
            _userUpdateDto.Town = _veryLongString;
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_PhoneNumber_Empty()
        {
            _userUpdateDto.PhoneNumber = "";
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_PhoneNumber_Is_Invalid()
        {
            _userUpdateDto.PhoneNumber = "ok 6543 6reg .., 34o";
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_PhoneNumber_TooLong()
        {
            _userUpdateDto.PhoneNumber = _veryLongString;
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Invalid_User_Not_Updated_DateOfBirth_Is_Invalid_DOB_Is_After_CurrentDate()
        {
            _userUpdateDto.DateOfBirth = DateTimeOffset.Now.AddYears(1);
            _updateUsersTableRequest.UsersToUpdate = new List<UserUpdateDto> { _userUpdateDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }

        #endregion

        #region REMOVING_USER
        [Fact]
        public async Task Valid_User_Removed()
        {
            _updateUsersTableRequest.UsersToRemove = new List<UserReadDto> { _userToRemoveDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeTrue();
            users.Count().ShouldBe(1);
        }

        [Fact]
        public async Task Invalid_User_Not_Removed_Wrong_Id()
        {
            _userToRemoveDto.Id = Guid.Parse("0303adb9-d4f5-44ef-a25f-667c98636fa1");
            _updateUsersTableRequest.UsersToRemove = new List<UserReadDto> { _userToRemoveDto };

            var result = await _handler.Handle(new UpdateUsersTableCommand(_updateUsersTableRequest), CancellationToken.None);
            var users = await _mockUow.Object.GetAllUsersAsync();

            result.ShouldBeOfType<UpdateUsersTableCommandResponse>();
            result.Success.ShouldBeFalse();
            users.Count().ShouldBe(2);
        }
        #endregion
    }
}
