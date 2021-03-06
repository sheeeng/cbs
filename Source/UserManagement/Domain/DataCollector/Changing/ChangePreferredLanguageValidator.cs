using Dolittle.Commands.Validation;
using FluentValidation;

namespace Domain.DataCollector.Changing
{
    public class ChangePreferredLanguageValidator : CommandInputValidatorFor<ChangePreferredLanguage>
    {
        public ChangePreferredLanguageValidator()
        {
            RuleFor(_ => _.PreferredLanguage)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .IsInEnum().WithMessage("Preferred Language must be valid");
        }
    }
}