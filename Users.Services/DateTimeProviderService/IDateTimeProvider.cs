namespace Users.Services.DateTimeProviderService
{
    public interface IDateTimeProvider
    {
        public DateTimeOffset DateTimeNow { get; }
    }
}
