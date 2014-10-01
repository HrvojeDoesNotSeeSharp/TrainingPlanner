using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Helpers
{
    public class DateFormatValidatorAttribute : RegularExpressionAttribute
    {
        public DateFormatValidatorAttribute()
            : base(@"[0-3][0-9]/[0-1][0-9]/20[12][0-9]")
        {
            ErrorMessage = "Please enter date in dd/mm/yyyy format";
        }

        public override bool IsValid(object value)
        {
            return true;
        }
    }
}