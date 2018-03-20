namespace BlissRecruitment.DataAccess.DTO
{
    using Ruiagf.Framework.Utils;
    using System;

    public class Question : DataTransferObject
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbUrl { get; set; }

        public string QuestionText { get; set; }

        public DateTime PublishedDatetime { get; set; }

        public Choice[] Choices { get; set; }
    }
}
