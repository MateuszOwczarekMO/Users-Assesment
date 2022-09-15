namespace Users.App.Dtos.Users
{
    public class UserUpdateDto
    {
        public Guid? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? StreetName { get; set; }

        public string? HouseNumber { get; set; }

        public string? ApartmentNumber { get; set; }

        public string? PostalCode { get; set; }

        public string? Town { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTimeOffset? DateOfBirth { get; set; }

        public UserUpdateDto()
        {

        }

        public UserUpdateDto(UserReadDto userReadDto)
        {
            Id = userReadDto.Id;
            FirstName = userReadDto.FirstName;
            LastName = userReadDto.LastName;
            StreetName = userReadDto.StreetName;
            HouseNumber = userReadDto.HouseNumber;
            ApartmentNumber = userReadDto.ApartmentNumber;
            PostalCode = userReadDto.PostalCode;
            Town = userReadDto.Town;
            PhoneNumber = userReadDto.PhoneNumber;
            DateOfBirth = userReadDto.DateOfBirth;
        }
    }
}
