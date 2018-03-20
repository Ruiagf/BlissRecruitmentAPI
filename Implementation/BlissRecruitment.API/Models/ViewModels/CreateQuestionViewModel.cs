namespace BlissRecruitment.Models.ViewModels
{
    using BlissRecruitment.Models.ViewModels.Validators;
    using FluentValidation.Attributes;

    [Validator(typeof(CreateQuestionViewModelValidator))]
    public class CreateQuestionViewModel
    {
        public string Image_url { get; set; }

        public string Thumb_url { get; set; }

        public string Question { get; set; }

        public string[] Choices { get; set; }
    }
}