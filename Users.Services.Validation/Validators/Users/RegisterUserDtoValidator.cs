using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Services.Validation.Validators.Users
{
    public class RegisterUserDtoValidator : AbstractValidator<UserRegisterDto>
    {

        public RegisterUserDtoValidator()
        {
            Include(new ILeaveRequestDtoValidator(_leaveTypeRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
