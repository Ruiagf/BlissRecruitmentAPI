namespace BlissRecruitment.Models
{
    using System;

    public class QuestionViewModel
    {
        public int Id { get; set; }

        public string Image_url { get; set; }

        public string Thumb_url { get; set; }

        [Newtonsoft.Json.JsonProperty("question")]
        public string QuestionText { get; set; }

        public DateTime Published_At { get; set; }

        public ChoiceViewModel[] Choices { get; set; }
    }
}