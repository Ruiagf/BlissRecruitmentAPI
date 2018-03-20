namespace BlissRecruitment.DataAccess.DTO
{
    public class CreateQuestion
    {
        public string ImageUrl { get; set; }

        public string ThumbUrl { get; set; }

        public string Question { get; set; }

        public string[] Choices { get; set; }
    }
}
