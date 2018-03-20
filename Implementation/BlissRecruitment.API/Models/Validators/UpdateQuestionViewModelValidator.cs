namespace BlissRecruitment.Models.Validators
{
    using BlissRecruitment.Models.ViewModels;
    using FluentValidation;

    public class UpdateQuestionViewModelValidator : AbstractValidator<UpdateQuestionViewModel>
    {
        private const int UrlMinLength = 5;
        private const int UrlMaxLength = 2000;

        private const int QuestionMinLength = 2;
        private const int QuestionMaxLength = 4000;

        public UpdateQuestionViewModelValidator()
        {
            RuleFor(x => x.Image_url).NotNull();
            RuleFor(x => x.Image_url).Length(UrlMinLength, UrlMaxLength);

            RuleFor(x => x.Thumb_url).NotNull();
            RuleFor(x => x.Thumb_url).Length(UrlMinLength, UrlMaxLength);

            RuleFor(x => x.Question).NotNull();
            RuleFor(x => x.Question).Length(QuestionMinLength, QuestionMaxLength);

            RuleFor(x => x.Choices).NotNull();

            RuleFor(x => x.Choices).SetCollectionValidator(new UpdateChoiceViewModelValidator());
        }
    }
}