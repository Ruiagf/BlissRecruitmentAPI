namespace BlissRecruitment.API.Tests
{
    using AutoMapper;
    using BlissRecruitment.API.Controllers;
    using BlissRecruitment.API.Tests.Fixtures;
    using BlissRecruitment.BusinessLogic.Abstract;
    using BlissRecruitment.Domain;
    using BlissRecruitment.Models;
    using BlissRecruitment.Models.ViewModels;
    using Moq;
    using Ruiagf.Framework.BaseUtils.HelperTypes;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Xunit;

    public class ShareControllerTests : IClassFixture<AutoMapperFixture>
    {
        private readonly AutoMapperFixture autoMapper;
        private readonly IMapper mapper;
        private readonly Mock<IShareProvider> mockProvider;

        public ShareControllerTests(AutoMapperFixture autoMapper)
        {
            this.autoMapper = autoMapper;
            this.mapper = this.autoMapper.Mapper;
            this.mockProvider = new Mock<IShareProvider>();
        }

        [Fact]
        public async Task MethodPostReturnsOkWhenViewModelIsValidAndProviderReturnsSuccess()
        {
            // Arrange
            var s = new ShareViewModel
            {
               Destination_email = "ruiagf@gmail.com",
               Content_url = "www.blissapplications.com"
            };

            this.mockProvider.Setup(_ => _.Share(It.IsAny<ContentToShare>())).Returns(new OperationResult(0));

            var controller = new ShareController(this.mapper, this.mockProvider.Object)
             {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
             };

            // Act
            var result = controller.Post(s);

            // Assert
            var contentResult = Assert.IsType<OkNegotiatedContentResult<ShareResult>>(result);
            Assert.Equal("OK", contentResult.Content.Status);
        }

        [Fact]
        public async Task MethodPostReturnsBadRequestWhenViewModelIsNotValid()
        {
            // Arrange
            var s = new ShareViewModel
            {
                Destination_email = "",
                Content_url = ""
            };

            // Arrange
            this.mockProvider.Setup(_ => _.Share(It.IsAny<ContentToShare>())).Returns(new OperationResult(-1));

            var controller = new ShareController(this.mapper, this.mockProvider.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var result = controller.Post(s);

            // Assert
            var contentResult = Assert.IsType<ResponseMessageResult>(result);
            contentResult.Response.TryGetContentValue(out ShareResult shareResult);

            Assert.Equal(HttpStatusCode.BadRequest, contentResult.Response.StatusCode);
            Assert.Equal("Bad Request. Either destination_email not valid or empty content_url", shareResult.Status);
        }
    }
}
