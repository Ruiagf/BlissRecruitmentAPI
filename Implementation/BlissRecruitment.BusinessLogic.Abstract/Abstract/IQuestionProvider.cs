namespace BlissRecruitment.BusinessLogic.Abstract
{
    using Domain;
    using Ruiagf.Framework.BaseUtils.HelperTypes;
    using System.Collections.Generic;

    public interface IQuestionProvider
    {
        OperationResult<IList<Question>> GetAllQuestions(int limit, int offset, string filter);

        OperationResult<Question> GetQuestionById(int id);

        OperationResult<Question> CreateNew(CreateQuestion question);

        OperationResult<Question> Update(Question question);
    }
}
