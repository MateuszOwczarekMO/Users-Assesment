namespace Users.App.Dtos.Users
{
    public class UserUpdateErrorDto
    {
        public UserUpdateDto UserUpdateDto { get; set; } = new UserUpdateDto();
        public List<string> Errors { get; set; }
    }
}
