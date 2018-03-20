namespace BlissRecruitment.Models.ViewModels.Validators
{
    using BlissRecruitment.Models.Validators;
    using FluentValidation;

    public class CreateQuestionViewModelValidator : AbstractValidator<CreateQuestionViewModel>
    {
        private const int UrlMinLength = 5;
        private const int UrlMaxLength = 2000;

        private const int QuestionMinLength = 2;
        private const int QuestionMaxLength = 4000;

        public CreateQuestionViewModelValidator()
        {
            RuleFor(x => x.Image_url).NotNull();
            RuleFor(x => x.Image_url).Length(UrlMinLength, UrlMaxLength);

            RuleFor(x => x.Thumb_url).NotNull();
            RuleFor(x => x.Thumb_url).Length(UrlMinLength, UrlMaxLength);

            RuleFor(x => x.Question).NotNull();
            RuleFor(x => x.Question).Length(QuestionMinLength, QuestionMaxLength);

            RuleFor(x => x.Choices).NotNull();
            RuleFor(x => x.Choices).Must(_ => _.Length > 1).WithMessage("There must be at least 2 choices to create a question");

            RuleFor(x => x.Choices).SetCollectionValidator(new CreateChoiceViewModelValidator());
        }
    }
}