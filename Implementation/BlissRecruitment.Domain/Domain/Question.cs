namespace BlissRecruitment.Domain
{
    using System;

    public class Question
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbUrl { get; set; }

        public string QuestionText { get; set; }

        public DateTime PublishedDatetime { get; set; }

        public Choice[] Choices { get; set; }
    }
}
