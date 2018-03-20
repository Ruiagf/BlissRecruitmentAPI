namespace BlissRecruitment.Models
{
    public class ChoiceViewModel
    {
        [Newtonsoft.Json.JsonProperty("choice")]
        public string ChoiceText { get; set; }

        public int Votes { get; set; }
    }
}