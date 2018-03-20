namespace BlissRecruitment.Models.Validators
{
    using BlissRecruitment.Models.ViewModels;
    using FluentValidation;

    public class QuestionsFilterViewModelValidator : AbstractValidator<QuestionsFilterViewModel>
    {
        private const int FilterMinumumLength = 2;
        private const int FilterMaxLength = 30;

        public QuestionsFilterViewModelValidator()
        {
            RuleFor(x => x.Limit).NotNull();

            RuleFor(x => x.Offset).NotNull();

            RuleFor(x => x.Filter).Length(FilterMinumumLength, FilterMaxLength).When(_ => _.Filter != null);
        }
    }
}