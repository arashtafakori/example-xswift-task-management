using XSwift.Mvc;
using Domain.TaskAggregation;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebMVCApp.ViewModels;
using Domain.SprintAggregation;
using Microsoft.AspNetCore.Mvc.Rendering;
using XSwift.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.WebMVCApp.Controllers
{
    [Authorize]
    public class Tasks : MvcControllerX
    {
        private HttpService ProjectApiService { get; set; }
        private HttpService SprintApiService { get; set; }
        private HttpService TaskApiService { get; set; }
        public Tasks(IHttpClientFactory httpClientFactory)
        {
            var httpClient = httpClientFactory.CreateClient(HttpClientNames.WebAPIClient);

            ProjectApiService = new HttpService(
                httpClient: httpClient,
                version: "v1",
                collectionResource: "Projects");

            SprintApiService = new HttpService(
                httpClient: httpClient,
                version: "v1",
                collectionResource: "Sprints");

            TaskApiService = new HttpService(
                httpClient: httpClient,
                version: "v1",
                collectionResource: "Tasks");
        }
        [Route($"{nameof(Projects)}/{{{nameof(Domain.TaskAggregation.GetTaskInfoList.ProjectId)}}}/[controller]", Name = nameof(GetTaskInfoList))]
        public async Task<IActionResult> GetTaskInfoList(
            GetTaskInfoListViewModel model,
            Guid projectId,
            Guid? sprintId = null,
            Domain.TaskAggregation.TaskStatus? status = null,
            string? descriptionSearchKey = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            model ??= new GetTaskInfoListViewModel();
            model.TaskInfoList =
                await TaskApiService.SendAsync<PaginatedViewModel<TaskInfo>>(
                    HttpMethod.Get,
                    version: "v1.1",
                    collectionResource: "Projects",
                    collectionItemParameter: projectId,
                    subCollectionResource: "Tasks",
                    queryParametersString: HttpContext.Request.QueryString.ToString());
            model.ProjectInfo = await ApiServiceFacilitator.GetTheProjectInfo(
                    ProjectApiService, projectId);
            model.TaskStatusSelectListItems = await GetSelectListOfTaskStatus();
            model.SprintsInfoItems = await GetSelectListOfSprintInfoList(projectId);

            return View(model);
        }

        public async Task<IActionResult> GetTheTaskInfo(Guid taskId)
        {
            var taskInfo = await ApiServiceFacilitator.GetTheTaskInfo(
                    TaskApiService, taskId);

            var model = new GetTheTaskInfoViewModel()
            {
                TaskInfo = taskInfo,
                ProjectInfo = await ApiServiceFacilitator.GetTheProjectInfo(
                    ProjectApiService, taskInfo.ProjectId)
            };
            return View(model);
        }

        public async Task<IActionResult> AddATask(Guid projectId)
        {
            var model = new AddATaskViewModel
            {
                ProjectId = projectId,
                ProjectInfo = await ApiServiceFacilitator.GetTheProjectInfo(
                    ProjectApiService, projectId),
                TaskStatusSelectListItems = await GetSelectListOfTaskStatus(),
                SprintsInfoItems = await GetSelectListOfSprintInfoList(projectId)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddingATaskConfirmed(
            AddATaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                await TaskApiService.SendAsync(HttpMethod.Post, model.ToRequest());

                return RedirectToRoute(
                    nameof(GetTaskInfoList),
                    new { projectId = model.ProjectId });
            }
            return View(model);
        }

        public async Task<IActionResult> EditTheTask(Guid taskId)
        {
            var taskInfo = await ApiServiceFacilitator.GetTheTaskInfo(
                    TaskApiService, taskId);

            var model = EditTheTaskViewModel.ToViewModel(taskInfo);
            model.TaskStatusSelectListItems = await GetSelectListOfTaskStatus();
            model.SprintsInfoItems = await GetSelectListOfSprintInfoList(taskInfo.ProjectId);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditingTheTaskConfirmed(
            EditTheTaskViewModel model, Guid projectId)
        {
            if (ModelState.IsValid)
            {
                await TaskApiService.SendAsync(
                    HttpMethod.Patch,
                    model.ToRequest(),
                    actionName: "EditTheTask");

                return RedirectToRoute(
                    nameof(GetTaskInfoList),
                    new { projectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ArchiveTheTask(Guid taskId)
        {
            var model = new ArchiveTheTaskViewModel
            {
                TaskInfo = await ApiServiceFacilitator.GetTheTaskInfo(
                    TaskApiService, taskId)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchivingTheTaskConfirmed(ArchiveTheTaskViewModel model)
        {
            await TaskApiService.SendAsync(
                HttpMethod.Patch,
                actionName: "ArchiveTheTask",
                collectionItemParameter: model.TaskInfo!.Id);

            return RedirectToRoute(
                 nameof(GetTaskInfoList),
                 new { model.TaskInfo!.ProjectId });
        }

        public async Task ChangeTheTaskStatus(Guid id, Domain.TaskAggregation.TaskStatus status)
        {
            await TaskApiService.SendAsync(
                HttpMethod.Patch,
                new ChangeTheTaskStatus(id, status),
                actionName: "ChangeTheTaskStatus");
        }

        private async Task<List<SelectListItem>> GetSelectListOfSprintInfoList(Guid projectId)
        {
            var sprintInfoList = await SprintApiService.SendAsync<PaginatedViewModel<SprintInfo>>(
                    HttpMethod.Get,
                    version: "v1.1",
                    collectionResource: "Projects",
                    collectionItemParameter: projectId,
                    subCollectionResource: "Sprints");

           return sprintInfoList.Items.ToSelectList(
                    value: item => item.Id!.ToString(),
                    text: item => item.Name);
        }
        private async Task<List<SelectListItem>> GetSelectListOfTaskStatus()
        {
            return
                (await TaskApiService.SendAsync<List<KeyValuePair<int, string>>>(
                    HttpMethod.Get,
                    actionName: "GetTaskStatusItems")).ToSelectList(
                    value: item => item.Key,
                    text: item => item.Value);
        }
    }
}
