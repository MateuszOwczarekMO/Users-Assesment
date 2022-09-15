using System.ComponentModel.DataAnnotations;
using Users.Services.DateTimeProviderService;

namespace Users.Services.Validation.ValidationAttributes
{
    public class DOBDateValidation : ValidationAttribute
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        public DOBDateValidation()
        {
            _dateTimeProvider = new DateTimeProvider();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTimeOffset date;
            bool parsed = DateTimeOffset.TryParse(value.ToString(), out date);
            if (!parsed)
                return new ValidationResult("Invalid Date");
            else
            {
                var now = _dateTimeProvider.DateTimeNow; 
                var msg = string.Format($"Please enter a date before {now}");

                try
                {
                    if (date >= now)
                        return new ValidationResult(msg);
                    else
                        return ValidationResult.Success;
                }
                catch (Exception e)
                {
                    return new ValidationResult(e.Message);
                }
            }
        }
    }
}
