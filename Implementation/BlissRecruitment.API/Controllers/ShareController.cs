namespace BlissRecruitment.API.Controllers
{
    using AutoMapper;
    using BlissRecruitment.BusinessLogic.Abstract;
    using BlissRecruitment.Domain;
    using BlissRecruitment.Models;
    using BlissRecruitment.Models.ViewModels;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class ShareController : ApiController
    {
        private readonly IMapper mapper;
        private readonly IShareProvider provider;

        public ShareController(IMapper mapper, IShareProvider provider)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        // POST: Share?destination_email={destination_email}&content_url={content_url}
        public IHttpActionResult Post([FromUri] ShareViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var res = this.provider.Share(this.mapper.Map<ContentToShare>(viewModel));

                if (res.Succeeded)
                {
                    return this.Ok(new ShareResult{ Status = "OK" });
                }
            }

            return this.ResponseMessage(this.Request.CreateResponse(HttpStatusCode.BadRequest, new ShareResult { Status = "Bad Request. Either destination_email not valid or empty content_url" }));
        }
    }
}
