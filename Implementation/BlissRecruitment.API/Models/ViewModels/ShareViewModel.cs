namespace BlissRecruitment.Models.ViewModels
{
    using BlissRecruitment.Models.Validators;
    using FluentValidation.Attributes;

    [Validator(typeof(ShareViewModelValidator))]
    public class ShareViewModel
    {
        public string Destination_email { get; set; }

        public string Content_url { get; set; }
    }
}