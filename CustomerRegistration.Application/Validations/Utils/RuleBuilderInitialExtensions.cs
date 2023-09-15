using CustomerRegistration.Application.Commands;
using FluentValidation;

namespace CustomerRegistration.Application.Validations.Utils
{
    public static class RuleBuilderInitialExtensions
    { 
        public static void ValidateName<T>(this IRuleBuilderInitial<T, string> src, string name) 
            where T : Command
        {
            src.NotNull().WithMessage($"Please ensure you have entered the {name}, cannot be null.")
                .NotEmpty().WithMessage($"Please ensure you have entered the {name}, cannot be empty.")
                .Length(1, 60).WithMessage($"The {name} must have between 2 and 60 characters.");
        }

        public static void ValidateNullOrEmpty<T1, T2>(this IRuleBuilderInitial<T1, T2> src, string name) 
            where T1 : Command
        {
            src.NotNull().WithMessage($"Please ensure you have entered the {name}, cannot be null.")
                .NotEmpty().WithMessage($"Please ensure you have entered the {name}, cannot be empty.");
        }
    }
}