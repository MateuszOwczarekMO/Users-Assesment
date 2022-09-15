using System.ComponentModel.DataAnnotations;

namespace Users.Domain.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(46)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(46)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(95)]
        public string StreetName { get; set; }

        [Required]
        [MaxLength(10)]
        public string HouseNumber { get; set; }

        [MaxLength(10)]
        public string? ApartmentNumber { get; set; }

        [Required]
        [MaxLength(12)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(35)]
        public string Town { get; set; }

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTimeOffset DateOfBirth { get; set; }

        [Required]
        [Range(0, 120)]
        public int Age { get; set; }
    }
}
