namespace Users.Application.Responses
{
    public class BaseQueryResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
    }
}
