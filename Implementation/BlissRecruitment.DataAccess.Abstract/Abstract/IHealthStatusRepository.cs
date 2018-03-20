namespace BlissRecruitment.DataAccess.Abstract
{
    using BlissRecruitment.DataAccess.DTO;

    public interface IHealthStatusRepository
    {
        HealthStatus GetHealthStatus();
    }
}
