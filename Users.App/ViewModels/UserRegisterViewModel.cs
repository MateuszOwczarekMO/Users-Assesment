using Users.App.Dtos.Users;

namespace Users.App.ViewModels
{
    public class UserRegisterViewModel
    {
        public UserRegisterDto UserRegisterDto { get; set; }
        public string DOB { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
