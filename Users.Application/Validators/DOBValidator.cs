namespace Users.Application.Validators
{
    public class DOBValidator
    {
        public static bool BeAValidDOB(DateTimeOffset? date)
        {
            var currentDate = DateTimeOffset.Now;

            if (date <= currentDate)
                return true;

            return false;
        }
    }
}
