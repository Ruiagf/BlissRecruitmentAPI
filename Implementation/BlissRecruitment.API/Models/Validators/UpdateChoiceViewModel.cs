namespace BlissRecruitment.Models.Validators
{
    using BlissRecruitment.Models.ViewModels;
    using FluentValidation;

    public class UpdateChoiceViewModelValidator : AbstractValidator<UpdateChoiceViewModel>
    {
        private const int ChoiceMinLength = 1;
        private const int ChoiceMaxLength = 4000;

        public UpdateChoiceViewModelValidator()
        {
            RuleFor(x => x.Choice).NotNull();
            RuleFor(x => x.Choice).Length(ChoiceMinLength, ChoiceMaxLength);

            RuleFor(x => x.Votes).GreaterThanOrEqualTo(0);
        }
    }
}