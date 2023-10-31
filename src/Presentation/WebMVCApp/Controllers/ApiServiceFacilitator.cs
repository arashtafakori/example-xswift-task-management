using Domain.ProjectAggregation;
using Domain.SprintAggregation;
using Domain.TaskAggregation;
using XSwift.Mvc;

namespace Presentation.WebMVCApp.Controllers
{
    public class ApiServiceFacilitator
    {
        public static async Task<ProjectInfo> GetTheProjectInfo(
            HttpService httpService, Guid projectId)
        {
            return await httpService.SendAsync<ProjectInfo>(
                HttpMethod.Get,
                collectionItemParameter: projectId);
        }

        public static async Task<SprintInfo> GetTheSprintInfo(
            HttpService httpService, Guid sprintId)
        {
            return await httpService.SendAsync<SprintInfo>(
                HttpMethod.Get,
                collectionItemParameter: sprintId);
        }

        public static async Task<TaskInfo> GetTheTaskInfo(
            HttpService httpService, Guid taskId)
        {
            return await httpService.SendAsync<TaskInfo>(
                HttpMethod.Get,
                collectionItemParameter: taskId);
        }
    }
}
