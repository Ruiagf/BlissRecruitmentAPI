namespace BlissRecruitment.Models.ViewModels
{
    using BlissRecruitment.Models.Validators;
    using FluentValidation.Attributes;

    [Validator(typeof(UpdateQuestionViewModelValidator))]
    public class UpdateQuestionViewModel
    {
        public string Image_url { get; set; }

        public string Thumb_url { get; set; }

        public string Question { get; set; }

        public UpdateChoiceViewModel[] Choices { get; set; }
    }
}