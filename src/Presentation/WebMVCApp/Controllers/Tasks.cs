using XSwift.Mvc;
using Module.Domain.TaskAggregation;
using Microsoft.AspNetCore.Mvc;
using Module.Presentation.WebMVCApp.ViewModels;
using Module.Domain.SprintAggregation;
using Microsoft.AspNetCore.Mvc.Rendering;
using XSwift.Domain;
using Microsoft.AspNetCore.Authorization;


namespace Module.Presentation.WebMVCApp.Controllers
{
    [Authorize]
    public class Tasks : XMvcController
    {
        private readonly HttpService _projectHttpService;
        private readonly HttpService _sprintHttpService;
        private readonly HttpService _taskHttpService;
        public Tasks(IHttpClientFactory httpClientFactory)
        {
            var httpClient = httpClientFactory.CreateClient(HttpClientNames.WebAPIClient);

            _projectHttpService = new HttpService(
                httpClient: httpClient,
                version: "v1",
                collectionResource: CollectionNames.Projects);

            _sprintHttpService = new HttpService(
                httpClient: httpClient,
                version: "v1",
                collectionResource: CollectionNames.Sprints);

            _taskHttpService = new HttpService(
                httpClient: httpClient,
                version: "v1",
                collectionResource: CollectionNames.Tasks);
        }
        [Route($"{nameof(Controllers.Projects)}/{{{nameof(Domain.TaskAggregation.GetTaskInfoList.ProjectId)}}}/[controller]", Name = nameof(GetTaskInfoList))]
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

            model.TaskInfoList = await _taskHttpService
                .SendAndReadAsResultAsync<PaginatedViewModel<TaskInfo>>(
                new XHttpRequest(HttpMethod.Get, version: "v1.1",
                collectionResource: CollectionNames.Projects,
                collectionItemParameter: projectId,
                subCollectionResource: CollectionNames.Tasks,
                queryParametersString: HttpContext.Request.QueryString.ToString()));

            model.ProjectInfo = await DataFacilitator.GetProjectInfo(_projectHttpService, projectId);
            model.TaskStatusSelectListItems = await GetSelectListOfTaskStatus();
            model.SprintsInfoItems = await GetSelectListOfSprintInfoList(projectId);

            return View(model);
        }

        public async Task<IActionResult> GetInfo(Guid taskId)
        {
            var taskInfo = await DataFacilitator.GetTaskInfo(_taskHttpService, taskId);

            var model = new GetTheTaskInfoViewModel()
            {
                TaskInfo = taskInfo,
                ProjectInfo = await DataFacilitator.GetProjectInfo(_projectHttpService, taskInfo.ProjectId)
            };
            return View(model);
        }

        public async Task<IActionResult> Add(Guid projectId)
        {
            var model = new AddATaskViewModel
            {
                ProjectId = projectId,
                ProjectInfo = await DataFacilitator.GetProjectInfo(_projectHttpService, projectId),
                TaskStatusSelectListItems = await GetSelectListOfTaskStatus(),
                SprintsInfoItems = await GetSelectListOfSprintInfoList(projectId)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddingConfirmed(
            AddATaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _taskHttpService.SendAsync(
                    new XHttpRequest(HttpMethod.Post, model.ToRequest()));

                return RedirectToRoute(
                    nameof(GetTaskInfoList),
                    new { projectId = model.ProjectId });
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid taskId)
        {
            var taskInfo = await DataFacilitator.GetTaskInfo(_taskHttpService, taskId);

            var model = EditTheTaskViewModel.ToViewModel(taskInfo);
            model.TaskStatusSelectListItems = await GetSelectListOfTaskStatus();
            model.SprintsInfoItems = await GetSelectListOfSprintInfoList(taskInfo.ProjectId);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditingConfirmed(
            EditTheTaskViewModel model, Guid projectId)
        {
            if (ModelState.IsValid)
            {
                await _taskHttpService.SendAsync(
                    new XHttpRequest(HttpMethod.Put, model.ToRequest()));

                return RedirectToRoute(
                    nameof(GetTaskInfoList),
                    new { projectId });
            }
            return View(model);
        }

        public async Task<IActionResult> Archive(Guid taskId)
        {
            var model = new ArchiveTheTaskViewModel
            {
                TaskInfo = await DataFacilitator.GetTaskInfo(_taskHttpService, taskId)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchivingConfirmed(ArchiveTheTaskViewModel model)
        {
            await _taskHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                actionName: HttpServiceBasicActionsName.Archive,
                collectionItemParameter: model.TaskInfo!.Id));

            return RedirectToRoute(
                 nameof(GetTaskInfoList),
                 new { model.TaskInfo!.ProjectId });
        }

        public async Task ChangeTheTaskStatus(Guid id, Domain.TaskAggregation.TaskStatus status)
        {
            await _taskHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                new ChangeTheTaskStatus(id, status),
                actionName: "ChangeTheTaskStatus"));
        }

        private async Task<List<SelectListItem>> GetSelectListOfSprintInfoList(Guid projectId)
        {
            var sprintInfoList = await _sprintHttpService
                .SendAndReadAsResultAsync<PaginatedViewModel<SprintInfo>>(
                new XHttpRequest(HttpMethod.Get, version: "v1.1",
                collectionResource: CollectionNames.Projects,
                collectionItemParameter: projectId,
                subCollectionResource: CollectionNames.Sprints));

           return sprintInfoList.Items.ToSelectList(
                    value: item => item.Id!.ToString(),
                    text: item => item.Name);
        }
        private async Task<List<SelectListItem>> GetSelectListOfTaskStatus()
        {
            var statusItems = await _taskHttpService
                .SendAndReadAsResultAsync<List<KeyValuePair<int, string>>>(
                new XHttpRequest(HttpMethod.Get, actionName: "GetTaskStatusItems"));

            return statusItems.ToSelectList(
                    value: item => item.Key,
                    text: item => item.Value);
        }
    }
}
