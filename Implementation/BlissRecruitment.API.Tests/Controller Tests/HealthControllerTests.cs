namespace BlissRecruitment.API.Tests
{
    using BlissRecruitment.API.Controllers;
    using BlissRecruitment.BusinessLogic.Abstract;
    using BlissRecruitment.Models;
    using Moq;
    using Ruiagf.Framework.BaseUtils.HelperTypes;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Xunit;

    public class HealthControllerTests
    {
        private readonly Mock<IHealthStatusProvider> mockProvider;

        public HealthControllerTests()
        {
            this.mockProvider = new Mock<IHealthStatusProvider>();
        }

        [Fact]
        public async Task MethodGetReturnsSuccessWhenHealthStatusProviderReturnsSuccess()
        {
            // Arrange
            this.mockProvider.Setup(_ => _.GetHealthStatus()).Returns(new OperationResult(0));

            var controller = new HealthController(this.mockProvider.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var result = controller.Get() as OkNegotiatedContentResult<HealthResult>;

            // Assert
            var contentResult = Assert.IsType<OkNegotiatedContentResult<HealthResult>>(result);
            Assert.Equal("OK", contentResult.Content.Status);
        }

        [Fact]
        public async Task MethodGetReturnsServiceUnavailableWhenHealthStatusProviderDoNotReturnsSuccess()
        {
            // Arrange
            this.mockProvider.Setup(_ => _.GetHealthStatus()).Returns(new OperationResult(-1));

            var controller = new HealthController(this.mockProvider.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act
            var result = controller.Get();

            // Assert
            var contentResult = Assert.IsType<ResponseMessageResult>(result);
            contentResult.Response.TryGetContentValue(out HealthResult healthResult);

            Assert.Equal(HttpStatusCode.ServiceUnavailable, contentResult.Response.StatusCode);
            Assert.Equal("Service Unavailable. Please try again later.", healthResult.Status);
        }
    }
}
