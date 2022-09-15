using Microsoft.AspNetCore.Components;
using System.Globalization;
using Users.App.Dtos.Users;
using Users.App.Requests.Commands.Users;
using Users.App.Services.Users;
using Users.App.ViewModels;

namespace Users.App.Pages.PageBases
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        private IUsersService _usersService { get; set; }

        public List<UserViewModel> Users = new List<UserViewModel>();
        public List<UserRegisterViewModel> UsersToRegister { get; set; } = new List<UserRegisterViewModel>();
        public List<Guid> UsersToUpdateIds { get; set; } = new List<Guid>();
        public List<UserReadDto> UsersToRemove { get; set; } = new List<UserReadDto>();

        public bool Loading = true;
        public bool Saving = false;
        public bool ShowSavingModal = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
            Loading = false;
        }

        public async Task LoadUsers()
        {
            var result = await _usersService.GetAllUsers();
            if (result.Success)
            {
                Users = new List<UserViewModel>();

                foreach (var u in result.Users)
                {
                    Users.Add(new UserViewModel
                    {
                        UserReadDto = u,
                        DOB = u.DateOfBirth.ToString("dd/mm/yyyy")
                    });
                }
            }
        }

        public void ToggleEditUser(Guid id)
        {
            var user = Users.FirstOrDefault(u => u.UserReadDto.Id == id);

            if (user.IsEdited)
            {
                UsersToUpdateIds.Remove(id);
                user.IsEdited = false;
                user.UserUpdateDto = new UserUpdateDto();
            }
            else
            {
                UsersToUpdateIds.Add(id);
                user.IsEdited = true;
                user.UserUpdateDto = new UserUpdateDto(user.UserReadDto);
            }
        }

        public void AddUserToRegister()
        {
            UsersToRegister.Add(new UserRegisterViewModel 
            { 
                UserRegisterDto = new UserRegisterDto { TempId = Guid.NewGuid() } 
            });
        }

        public void RemoveUserToRegister(Guid id)
        {
            var userToRegister = UsersToRegister.First(u => u.UserRegisterDto.TempId == id);
            UsersToRegister.Remove(userToRegister);
        }

        public void AddUserToRemove(UserReadDto userReadDto)
        {
            UsersToRemove.Add(userReadDto);
            var user = Users.FirstOrDefault(u => u.UserReadDto.Id == userReadDto.Id);
            user.Removed = true;
        }

        public void ToggleSaveModal()
        {
            ShowSavingModal = !ShowSavingModal;
        }

        public void Cancel()
        {
            foreach(var user in UsersToRemove)
            {
                var userToRemove = Users.FirstOrDefault(u => u.UserReadDto.Id == user.Id);
                userToRemove.Removed = false;
            }

            UsersToRegister = new List<UserRegisterViewModel>();
            UsersToRemove = new List<UserReadDto>();
            UsersToUpdateIds = new List<Guid>();
        }

        public async Task Save()
        {
            Saving = true;

            var usersToUpdate = new List<UserUpdateDto>();

            foreach(var userToUpdateId in UsersToUpdateIds)
            {
                var userToUpdate = Users.FirstOrDefault(u => u.UserReadDto.Id == userToUpdateId);
                userToUpdate.Errors = new List<string>();

                DateTimeOffset dateTime;
                if (DateTimeOffset.TryParseExact(userToUpdate.DOB, "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    userToUpdate.UserUpdateDto.DateOfBirth = DateTimeOffset.Parse(userToUpdate.DOB);
                    usersToUpdate.Add(userToUpdate.UserUpdateDto);
                }
                else
                {
                    userToUpdate.Errors.Add("Invalid date format!");
                    ShowSavingModal = false;
                    Saving = false;
                    return;
                }
            }

            var usersRoRegister = UsersToRegister.Select(c => { c.Errors = new List<string>(); return c.UserRegisterDto; }).ToList();

            var request = new UpdateUsersTableRequest
            {
                UsersToRegister = usersRoRegister,
                UsersToRemove = UsersToRemove,
                UsersToUpdate = usersToUpdate
            };

            var result = await _usersService.UpdateUsersTable(request);

            if (result.Success)
            {
                Cancel();
                await LoadUsers();
            }
            else
            {
                foreach(var registerError in result.UsersToRegisterErrors)
                {
                    var userToRegister = UsersToRegister.FirstOrDefault(u => u.UserRegisterDto.TempId == registerError.UserRegisterDto.TempId);
                    userToRegister.Errors = registerError.Errors;
                }

                foreach (var updateError in result.UsersToUpdateErrors)
                {
                    var userToUpdate = Users.FirstOrDefault(u => u.UserReadDto.Id == updateError.UserUpdateDto.Id);
                    userToUpdate.Errors = updateError.Errors;
                }
            }

            ShowSavingModal = false;
            Saving = false;
        }
    }
}
