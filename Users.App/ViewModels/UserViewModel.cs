using Users.App.Dtos.Users;

namespace Users.App.ViewModels
{
    public class UserViewModel
    {
        public UserReadDto UserReadDto { get; set; }
        public UserUpdateDto UserUpdateDto { get; set; }
        public string DOB { get; set; }
        public bool IsEdited { get; set; } = false;
        public bool Removed { get; set; } = false;
        public List<string> Errors { get; set; }
    }
}
