namespace Users.Services.DateTimeProviderService
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset DateTimeNow => DateTimeOffset.Now;
    }
}
