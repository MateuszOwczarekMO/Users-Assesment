using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Users.Domain.Models;
using Users.Repositories.Users;

namespace Users.Tests.Mocks
{
    public static class MockUserRepository
    {
        public static Mock<IUserRepository> GetUsersRepository()
        {
            var users = new List<User>
            {
                //User to update in tests
                new User
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
                },
                // User to remove in tests
                new User
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
                },
            };

            var mockRepo = new Mock<IUserRepository>();

            mockRepo.Setup(r => r.GetAllUsersAsync()).ReturnsAsync(users);

            mockRepo.Setup(r => r.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                return users.FirstOrDefault(u => u.Id == id);
            });

            mockRepo.Setup(r => r.RegisterRangeOfUsersAsync(It.IsAny<IEnumerable<User>>())).ReturnsAsync((List<User> usersToRegister) =>
            {
                foreach(var userToRegister in usersToRegister)
                {
                    users.Add(userToRegister);
                }
                return true;
            });

            mockRepo.Setup(r => r.UpdateRangeOfUsersAsync(It.IsAny<IEnumerable<User>>())).ReturnsAsync((List<User> usersToEdit) =>
            {
                foreach(var userToEdit in usersToEdit)
                {
                    var user = users.FirstOrDefault(u => u.Id == userToEdit.Id);
                    user = userToEdit;
                }
                return true;
            });
            
            mockRepo.Setup(r => r.RemoveRangeOfUsersAsync(It.IsAny<IEnumerable<User>>())).ReturnsAsync((List<User> usersToRemove) =>
            {
                foreach (var userToEdit in usersToRemove)
                {
                    var user = users.FirstOrDefault(u => u.Id == userToEdit.Id);
                    if(user != null)
                    {
                        users.Remove(user);
                    }
                }
                return true;
            });
            
            mockRepo.Setup(r => r.HasUserAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                return users.Exists(u => u.Id == id);
            });

            return mockRepo;
        }
    }
}
