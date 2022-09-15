using Users.Services.DateTimeProviderService;

namespace Users.Services.GenerateAgeService
{
    public class GenerateAgeFromDOB : IGenerateAgeFromDOB
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        public GenerateAgeFromDOB(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public int GenerateAge(DateTimeOffset dateOfBirth)
        {
            var today = _dateTimeProvider.DateTimeNow;

            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth > today.AddYears(-age)) age--;

            return age;
        }
    }
}
