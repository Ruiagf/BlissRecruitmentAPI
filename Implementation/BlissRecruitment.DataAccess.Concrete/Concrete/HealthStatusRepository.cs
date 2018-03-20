namespace BlissRecruitment.DataAccess.Concrete
{
    using BlissRecruitment.DataAccess.DTO;
    using BlissRecruitment.DataAccess.Abstract;
    using Ruiagf.Framework.Utils;
    using System.Linq;

    public class HealthStatusRepository : IHealthStatusRepository
    {
        private readonly string connectionString;

        public HealthStatusRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public HealthStatus GetHealthStatus()
        {
            var res = DalUtilMethods.GetList<HealthStatus>("[dbo].[sp_GetHealthStatus]", this.connectionString);
            
            return res.First();
        }
    }
}
