namespace BlissRecruitment.API.Controllers
{
    using AutoMapper;
    using BlissRecruitment.BusinessLogic.Abstract;
    using BlissRecruitment.Domain;
    using BlissRecruitment.Models;
    using Models.ViewModels;
    using Ruiagf.Framework.BaseUtils.HelperTypes;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class QuestionsController : ApiController
    {
        private readonly IMapper mapper;
        private readonly IQuestionProvider provider;

        public QuestionsController(IMapper mapper, IQuestionProvider provider)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        // GET Questions?{limit}&{offset}&{filter}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit">The number of records to retrieve</param>
        /// <param name="offset">0 based starting index of the first retrieved record.
        /// If you invoked with limit=5 then you should use offset=5 to obtain the next records. 
        /// If you asked for 5 but only got 4, e.g., that means there are no more records to show.</param>
        /// <param name="filter">Use this field to search for the filter pattern on "question" and "choice" properties.The search will perform a "lowercase contains" strategy on those fields to retrieve results.</param>
        public IHttpActionResult Get([FromUri]QuestionsFilterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var res = this.provider.GetAllQuestions(viewModel.Limit.Value, viewModel.Offset.Value, viewModel.Filter);

                if (res.Succeeded)
                {
                    return this.Ok<IEnumerable<QuestionViewModel>>(this.mapper.Map<IEnumerable<QuestionViewModel>>(res.Result));
                }

                return this.Ok(new QuestionViewModel[0]);
            }

            return this.BadRequest();
        }

        // GET Questions/5
        /// <summary>
        /// Obtain a question through id
        /// </summary>
        /// <param name="id">id of the question to retrieve</param>
        public IHttpActionResult Get([FromUri] int id)
        {
            var res = this.provider.GetQuestionById(id);

            if (res.Succeeded)
            {
                if (res.Result == null)
                {
                    return this.ResponseMessage(this.Request.CreateResponse(HttpStatusCode.NotFound, new ResultBase{ Status = $"Question with id {id} not found." }));
                }

                return this.Ok(this.mapper.Map<QuestionViewModel>(res.Result));
            }

            return this.BadRequest();
        }

        // POST Questions
        /// <summary>
        /// You may create your own question using this action. 
        /// It takes a JSON object containing a question and a collection of answers in the form of choices.
        /// </summary>
        public IHttpActionResult Post([FromBody]CreateQuestionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var res = this.mapper.Map<OperationResult<QuestionViewModel>>(this.provider.CreateNew(this.mapper.Map<CreateQuestion>(viewModel)));

                if (res.Succeeded)
                {
                    var response = this.Request.CreateResponse(HttpStatusCode.Created, res.Result);
                    var uri = this.Url.Link("DefaultApi", new { id = res.Result.Id });
                    response.Headers.Location = new Uri(uri);
                    return this.ResponseMessage(response);
                }
            }

            return this.BadRequest();
        }

        // PUT Questions/5
        /// <summary>
        /// You may update a question using this action. 
        /// It takes a JSON object containing a question and a collection of answers in the form of choices.
        /// </summary>
        public IHttpActionResult Put(int id, [FromBody] UpdateQuestionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var res = this.mapper.Map<OperationResult<QuestionViewModel>>(this.provider.Update(this.mapper.Map<Question>(viewModel)));

                if (res.Succeeded)
                {
                    var response = this.Request.CreateResponse(HttpStatusCode.Created, res.Result);
                    var uri = this.Url.Link("DefaultApi", new { id = res.Result.Id });
                    response.Headers.Location = new Uri(uri);
                    return this.ResponseMessage(response);
                }
            }

            return this.ResponseMessage(this.Request.CreateResponse(HttpStatusCode.BadRequest, new ResultBase { Status = "Bad Request. All fields are mandatory." }));
        }
    }
}