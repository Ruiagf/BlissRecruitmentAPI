namespace BlissRecruitment.DataAccess.DTO
{
    using Ruiagf.Framework.Utils;

    public class HealthStatus : DataTransferObject
    {
        public short State { get; set; }

        public string Description { get; set; }
    }
}
