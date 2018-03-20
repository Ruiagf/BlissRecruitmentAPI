namespace BlissRecruitment.API.Tests
{
    using AutoMapper;
    using BlissRecruitment.API.Controllers;
    using BlissRecruitment.API.Tests.Fixtures;
    using BlissRecruitment.BusinessLogic.Abstract;
    using BlissRecruitment.Domain;
    using BlissRecruitment.Models;
    using Moq;
    using Ruiagf.Framework.BaseUtils.HelperTypes;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Xunit;

    public class QuestionsControllerTests : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture autoMapper;
        private readonly IMapper mapper;
        private readonly Mock<IQuestionProvider> mockProvider;

        public QuestionsControllerTests(AutoMapperFixture autoMapper)
        {
            this.autoMapper = autoMapper;
            this.mapper = this.autoMapper.Mapper;
            this.mockProvider = new Mock<IQuestionProvider>();
        }

        [Fact]
        public async Task MethodGetReturnsCorrectResultsWhenIdExists()
        {
            // Arrange
            this.mockProvider.Setup(_ => _.GetQuestionById(It.IsAny<int>())).Returns(new OperationResult<Question>(0, new Question
            {
                Id = 1,
                ImageUrl = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)",
                ThumbUrl = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)",
                PublishedDatetime = DateTime.Now,
                QuestionText = "Favourite programming language?",
                Choices = new Choice[] {
                    new Choice { Id = 1, ChoiceText = "C#", Votes = 20 },
                    new Choice { Id = 2, ChoiceText = "Java", Votes = 15 },
                    new Choice { Id = 3, ChoiceText = "C", Votes = 1 }
                }
            }));

            var id = 1;

            var controller = new QuestionsController(this.mapper, this.mockProvider.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var result = controller.Get(id) as OkNegotiatedContentResult<QuestionViewModel>;

            // Assert
            var contentResult = Assert.IsType<OkNegotiatedContentResult<QuestionViewModel>>(result);

            Assert.Equal(id, contentResult.Content.Id);
        }


        [Fact]
        public async Task MethodGetReturnsNotFoundIfIdDoNotExists()
        {
            // Arrange
            this.mockProvider.Setup(_ => _.GetQuestionById(It.IsAny<int>())).Returns(new OperationResult<Question>(0, null));

            var id = 1;

            var controller = new QuestionsController(this.mapper, this.mockProvider.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var result = controller.Get(id);

            // Assert
            var contentResult = Assert.IsType<ResponseMessageResult>(result);
            contentResult.Response.TryGetContentValue(out ResultBase r);

            Assert.Equal(HttpStatusCode.NotFound, contentResult.Response.StatusCode);
            Assert.Equal("Question with id 1 not found.", r.Status);
        }
    }
}
