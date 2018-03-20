namespace BlissRecruitment.BusinessLogic.Concrete
{
    using AutoMapper;
    using BlissRecruitment.BusinessLogic.Abstract;
    using BlissRecruitment.DataAccess.Abstract;
    using Domain;
    using Ruiagf.Framework.BaseUtils.HelperTypes;
    using System;
    using System.Collections.Generic;

    public class QuestionProvider : IQuestionProvider
    {
        private readonly IMapper mapper;
        private readonly IQuestionRepository repository;

        public QuestionProvider(IMapper mapper, IQuestionRepository repository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public OperationResult<Question> CreateNew(CreateQuestion question)
        {
            var res = this.repository.CreateNew(this.mapper.Map<DataAccess.DTO.CreateQuestion>(question));

            if (res.Succeeded)
            {
                return this.mapper.Map<OperationResult<Question>>(this.repository.GetQuestionById(res.Result));
            }

            return new OperationResult<Question>(res.Errors);
        }

        public OperationResult<IList<Question>> GetAllQuestions(int limit, int offset, string filter)
        {
            var res = this.repository.GetAllQuestions(limit, offset, filter);
            return this.mapper.Map<OperationResult<IList<Question>>>(res);
        }

        public OperationResult<Question> GetQuestionById(int id)
        {
            return this.mapper.Map<OperationResult<Question>>(this.repository.GetQuestionById(id));
        }

        public OperationResult<Question> Update(Question question)
        {
            var res = this.repository.Update(this.mapper.Map<DataAccess.DTO.Question>(question));

            if (res.Succeeded)
            {
                return this.mapper.Map<OperationResult<Question>>(this.repository.GetQuestionById(question.Id));
            }

            return new OperationResult<Question>(res.Errors);
        }
    }
}
