namespace BlissRecruitment.DataAccess.Abstract
{
    using BlissRecruitment.DataAccess.DTO;
    using Ruiagf.Framework.BaseUtils.HelperTypes;
    using System.Collections.Generic;

    public interface IQuestionRepository
    {
        OperationResult<IList<Question>> GetAllQuestions(int limit, int offset, string filter);

        OperationResult<Question> GetQuestionById(int id);

        OperationResult<int> CreateNew(CreateQuestion question);

        OperationResult Update(Question question);
    }
}
