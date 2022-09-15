namespace Users.Services.GenerateAgeService
{
    public interface IGenerateAgeFromDOB
    {
        public int GenerateAge(DateTimeOffset dateOfBirth);
    }
}
