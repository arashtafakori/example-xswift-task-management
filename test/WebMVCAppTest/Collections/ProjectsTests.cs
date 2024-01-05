//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using Presentation.WebMVCApp.ViewModels;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Xunit;

//namespace WebMVCAppTest
//{
//    public class ProjectsTests : IClassFixture<ProjectFixture>
//    {
//        private IServiceScope _serviceScope;
//        private readonly ProjectFixture _fixture;
//        public ProjectsTests(ProjectFixture fixture)
//        {
//            _fixture = fixture;
//            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
//        }
//        [Fact]
//        public async Task GetProjectInfoList_ReturnsAViewResult_WithAListOfProjectInfo()
//        {
//            var httpClientFactory = _serviceScope.ServiceProvider.GetRequiredService<IHttpClientFactory>();

//            // Arrange
//            var controller = new Presentation.WebMVCApp.Controllers.Projects(httpClientFactory);

//            // Act
//            var result = await controller.GetProjectInfoList();

//            // Assert
//            var viewResult = Assert.IsType<ViewResult>(result);
//            var model = Assert.IsAssignableFrom<IEnumerable<GetProjectInfoListViewModel>>(
//                viewResult.ViewData.Model);
//            Assert.Equal(2, model.Count());
//        }
//    }
//}
