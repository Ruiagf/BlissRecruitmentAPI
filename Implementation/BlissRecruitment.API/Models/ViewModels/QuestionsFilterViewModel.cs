namespace BlissRecruitment.Models.ViewModels
{
    using BlissRecruitment.Models.Validators;
    using FluentValidation.Attributes;

    [Validator(typeof(QuestionsFilterViewModelValidator))]
    public class QuestionsFilterViewModel
    {
        public int? Limit { get; set; }

        public int? Offset { get; set; }
        
        public string Filter { get; set; }
    }
}