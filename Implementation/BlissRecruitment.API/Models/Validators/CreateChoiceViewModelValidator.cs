namespace BlissRecruitment.Models.Validators
{
    using FluentValidation;

    public class CreateChoiceViewModelValidator : AbstractValidator<string>
    {
        private const int ChoiceMinLength = 1;
        private const int ChoiceMaxLength = 4000;

        public CreateChoiceViewModelValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().Length(ChoiceMinLength,ChoiceMaxLength);
        }
    }
}