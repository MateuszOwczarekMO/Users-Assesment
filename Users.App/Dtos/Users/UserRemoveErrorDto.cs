namespace Users.App.Dtos.Users
{
    public class UserRemoveErrorDto
    {
        public UserReadDto UserToRemove { get; set; } = new UserReadDto();
        public List<string> Errors { get; set; }
    }
}
