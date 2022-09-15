namespace Users.App.Dtos.Users
{
    public class UserRegisterErrorDto
    {
        public UserRegisterDto UserRegisterDto { get; set; } = new UserRegisterDto();
        public List<string> Errors { get; set; }
    }
}
