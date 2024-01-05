using Module.Domain.ProjectAggregation;
using Module.Domain.SprintAggregation;
using Module.Domain.TaskAggregation;
using XSwift.Mvc;

namespace Module.Presentation.WebMVCApp.Controllers
{
    public static class DataFacilitator
    {
        public static async Task<ProjectInfo> GetProjectInfo(
            HttpService httpService, Guid id)
        {
            return await httpService.SendAndReadAsResultAsync<ProjectInfo>(
                new XHttpRequest(HttpMethod.Get,
                collectionItemParameter: id));
        }

        public static async Task<SprintInfo> GetSprintInfo(
            HttpService httpService, Guid id)
        {
            return await httpService.SendAndReadAsResultAsync<SprintInfo>(
                new XHttpRequest(HttpMethod.Get,
                collectionItemParameter: id));
        }

        public static async Task<TaskInfo> GetTaskInfo(
            HttpService httpService, Guid id)
        {
            return await httpService.SendAndReadAsResultAsync<TaskInfo>(
                new XHttpRequest(HttpMethod.Get,
                collectionItemParameter: id));
        }
    }
}
