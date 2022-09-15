namespace Users.App.Dtos.Users
{
    public class UserRegisterDto
    {
        public Guid? TempId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? StreetName { get; set; }

        public string? HouseNumber { get; set; }

        public string? ApartmentNumber { get; set; }

        public string? PostalCode { get; set; }

        public string? Town { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTimeOffset? DateOfBirth { get; set; }
    }
}
