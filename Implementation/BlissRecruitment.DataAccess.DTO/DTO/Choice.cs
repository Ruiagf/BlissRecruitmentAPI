namespace BlissRecruitment.DataAccess.DTO
{
    using Ruiagf.Framework.Utils;

    public class Choice : DataTransferObject
    {
        public int Id { get; set; }

        public string ChoiceText { get; set; }

        public int Votes { get; set; }
    }
}
