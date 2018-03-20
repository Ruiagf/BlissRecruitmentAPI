namespace BlissRecruitment.Models.Validators
{
    using BlissRecruitment.Models.ViewModels;
    using FluentValidation;

    public class ShareViewModelValidator : AbstractValidator<ShareViewModel>
    {
        private const int UrlMinLength = 5;
        private const int UrlMaxLength = 2000;

        public ShareViewModelValidator()
        {
            RuleFor(x => x.Destination_email).NotNull();
            RuleFor(x => x.Destination_email).EmailAddress();

            RuleFor(x => x.Content_url).NotNull();
            RuleFor(x => x.Content_url).Length(UrlMinLength, UrlMaxLength);
        }
    }
}