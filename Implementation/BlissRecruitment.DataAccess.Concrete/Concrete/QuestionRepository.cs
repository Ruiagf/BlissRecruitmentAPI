namespace BlissRecruitment.DataAccess.Concrete
{
    using BlissRecruitment.DataAccess.Abstract;
    using BlissRecruitment.DataAccess.DTO;
    using Ruiagf.Framework.BaseUtils.HelperTypes;
    using Ruiagf.Framework.Utils;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Transactions;

    public class QuestionRepository : IQuestionRepository
    {
        private readonly string connectionString;

        public QuestionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public OperationResult<int> CreateNew(CreateQuestion question)
        {
            var sqlParameterList = new[]
            {
                new SqlParameter { ParameterName = "@ImageUrl", Value = question.ImageUrl },
                new SqlParameter { ParameterName = "@ThumbUrl", Value = question.ThumbUrl },
                new SqlParameter { ParameterName = "@QuestionText", Value = question.Question },
                new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
            };

            int? id = null;

            using (var t = new TransactionScope())
            {
                var result = DalUtilMethods.Execute($"[dbo].[sp_InsertQuestion]", this.connectionString, sqlParameterList);

                if (result != 0)
                {
                    return new OperationResult<int>("Something went wrong while creating question");
                }

                id = Convert.ToInt32(sqlParameterList[3].Value);

                foreach (var c in question.Choices)
                {
                    var res = DalUtilMethods.Execute(
                        "[dbo].[sp_InsertChoice]", 
                        this.connectionString, 
                        new[]
                        {
                            new SqlParameter { ParameterName = "@QuestionId", Value = id.Value },
                            new SqlParameter { ParameterName = "@ChoiceText", Value = c }
                        });

                    if (res != 0)
                    {
                        return new OperationResult<int>("Something went wrong while creating choices");
                    }
                }

                t.Complete();
            }

            return new OperationResult<int>(0, id.Value);
        }

        public OperationResult<IList<Question>> GetAllQuestions(int limit, int offset, string filter)
        {
            var sqlParameterList = new[]
            {
                new SqlParameter("@MaximumRows", limit),
                new SqlParameter("@StartRowIndex", offset),
                new SqlParameter("@Text", filter),
                new SqlParameter { ParameterName = "@Count", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
            };

            var res = DalUtilMethods.GetList<Question>("[dbo].[sp_GetAllQuestions]", this.connectionString, sqlParameterList);

            foreach(var q in res)
            {
                q.Choices = this.GetChoicesByQuestionId(q.Id).ToArray();
            }

            return new OperationResult<IList<Question>>(0, res);
        }

        public OperationResult<Question> GetQuestionById(int id)
        {
            var sqlParameterList = new[]
            {
                new SqlParameter("@Id", id)
            };

            var res = DalUtilMethods.GetList<Question>("[dbo].[sp_GetQuestionById]", this.connectionString, sqlParameterList);
            
            if (res.Count > 0)
            {
                var q = res.First();

                q.Choices = this.GetChoicesByQuestionId(q.Id).ToArray();
            }
            
            return new OperationResult<Question>(0, res.Count > 0 ? res.First() : null);
        }

        public OperationResult Update(Question question)
        {
            // TODO: implement update. 
            // Now, simulate that everything went ok on the update
            return new OperationResult(0);
        }

        private IList<Choice> GetChoicesByQuestionId(int id)
        {
            var sqlParameterList = new[]
            {
                new SqlParameter("@QuestionId", id)
            };

           return DalUtilMethods.GetList<Choice>("[dbo].[sp_GetChoicesByQuestionId]", this.connectionString, sqlParameterList);
        }
    }
}
