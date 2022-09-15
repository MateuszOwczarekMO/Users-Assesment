using FluentValidation;
using Users.Application.Dtos.Users;

namespace Users.Application.Validators.Users
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(u => u.TempId).NotNull().WithMessage("{PropertyName} must be present")
                .NotEmpty().WithMessage("{PropertyName} must be not empty");

            RuleFor(u => u.FirstName).NotNull().WithMessage("{PropertyName} must be present")
                .NotEmpty().WithMessage("{PropertyName} must be not empty")
                .MaximumLength(46).WithMessage("{PropertyName} is too long!")
                .Matches("^[a-zA-Z]+$").WithMessage("{PropertyName} must have no numbers or spaces!");

            RuleFor(u => u.LastName).NotNull().WithMessage("{PropertyName} must be present")
                .NotEmpty().WithMessage("{PropertyName} must be not empty")
                .MaximumLength(46).WithMessage("{PropertyName} is too long!")
                .Matches("^[a-zA-Z]+$").WithMessage("{PropertyName} must have no numbers or spaces!");

            RuleFor(u => u.StreetName).NotNull().WithMessage("{PropertyName} must be present")
                .NotEmpty().WithMessage("{PropertyName} must be not empty")
                .MaximumLength(95).WithMessage("{PropertyName} is too long!");

            RuleFor(u => u.HouseNumber).NotNull().WithMessage("{PropertyName} must be present")
                .NotEmpty().WithMessage("{PropertyName} must be not empty")
                .MaximumLength(10).WithMessage("{PropertyName} is too long!")
                .Matches("^\\d+[a-zA-Z]*$").WithMessage("{PropertyName} is invalid!");

            RuleFor(u => u.ApartmentNumber)
                .Matches("^\\d+[a-zA-Z]*$").WithMessage("{PropertyName} is invalid!").When(u => !string.IsNullOrWhiteSpace(u.ApartmentNumber))
                .MaximumLength(10).WithMessage("{PropertyName} is too long!").When(u => !string.IsNullOrWhiteSpace(u.ApartmentNumber));

            RuleFor(u => u.PostalCode).NotNull().WithMessage("{PropertyName} must be present")
                .NotEmpty().WithMessage("{PropertyName} must be not empty")
                .MaximumLength(12).WithMessage("{PropertyName} is too long!");

            RuleFor(u => u.Town).NotNull().WithMessage("{PropertyName} must be present")
                .NotEmpty().WithMessage("{PropertyName} must be not empty")
                .MaximumLength(35).WithMessage("{PropertyName} is too long!");

            RuleFor(u => u.PhoneNumber).NotNull().WithMessage("{PropertyName} must be present")
                .NotEmpty().WithMessage("{PropertyName} must be not empty")
                .MaximumLength(15).WithMessage("{PropertyName} is too long!")
                .Matches("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$").WithMessage("{PropertyName} invalid!");

            RuleFor(u => u.DateOfBirth).NotNull().WithMessage("{PropertyName} must be present")
                .Must(DOBValidator.BeAValidDOB).WithMessage("Invalid {PropertyName}").When(u => u.DateOfBirth != null);
        }
    }
}
