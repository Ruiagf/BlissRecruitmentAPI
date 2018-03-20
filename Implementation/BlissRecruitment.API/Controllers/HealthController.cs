namespace BlissRecruitment.API.Controllers
{
    using BlissRecruitment.Models;
    using BusinessLogic.Abstract;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class HealthController : ApiController
    {
        private readonly IHealthStatusProvider provider;

        public HealthController(IHealthStatusProvider provider)
        {
            this.provider = provider ?? throw new System.ArgumentNullException(nameof(provider));
        }

        // GET Health
        public IHttpActionResult Get()
        {
            var res = this.provider.GetHealthStatus();
            
            if (res.Succeeded)
            {
                return this.Ok(new HealthResult { Status = "OK" });
            }

            return this.ResponseMessage(this.Request.CreateResponse(HttpStatusCode.ServiceUnavailable, new HealthResult { Status = "Service Unavailable. Please try again later." }));
        }
    }
}