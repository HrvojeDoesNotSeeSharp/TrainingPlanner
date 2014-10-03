using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Helpers
{
    public class DateFormatValidatorAttribute : RegularExpressionAttribute
    {
        public DateFormatValidatorAttribute()
            : base(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$")
        {
            ErrorMessage = "Please enter date in dd/mm/yyyy format";
        }

        public override bool IsValid(object value)
        {
            return true;
        }
    }
}