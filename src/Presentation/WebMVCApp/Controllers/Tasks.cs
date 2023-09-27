using Contract;
using CoreX.Mvc;
using Domain.TaskAggregation;
using Domain.ProjectAggregation;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebMVCApp.ViewModels;
using Domain.SprintAggregation;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreX.Domain;

namespace Presentation.WebMVCApp.Controllers
{
    public class Tasks : XMvcController
    {
        private readonly IProjectService _projectService;
        private readonly ISprintService _sprintService;
        private readonly ITaskService _taskService;
        public Tasks(
            IProjectService projectService,
            ISprintService sprintService,
            ITaskService taskService)
        {
            _projectService = projectService;
            _sprintService = sprintService;
            _taskService = taskService;
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
            var request = GetRequest<GetTaskInfoList>()
                .SetProjectId(projectId);
            request.Setup(
                paginationSetting: new PaginationSetting(
                    defaultPageNumber: 1, defaultPageSize: 10));

            model ??= new GetTaskInfoListViewModel();
            model.TaskInfoList = await _taskService.Process(request);
            model.ProjectInfo = await _projectService.Process(new GetTheProjectInfo(projectId));
            model.TaskStatusSelectListItems = await GetSelectListOfTaskStatus();
            model.SprintsInfoItems = await GetSelectListOfSprintInfoList(projectId);

            return View(model);
        }

        public async Task<IActionResult> GetTheTaskInfo(Guid id)
        {
            var taskInfo = await _taskService.Process(
                new GetTheTaskInfo(id));
            var model = new GetTheTaskInfoViewModel()
            {
                TaskInfo = taskInfo,
                ProjectInfo = await _projectService.Process(
                    new GetTheProjectInfo(taskInfo!.ProjectId))
            };
            return View(model);
        }

        public async Task<IActionResult> AddATask(Guid projectId)
        {
            var model = new AddATaskViewModel
            {
                ProjectId = projectId,
                ProjectInfo = await _projectService.Process(new GetTheProjectInfo(projectId)),
                TaskStatusSelectListItems = await GetSelectListOfTaskStatus(),
                SprintsInfoItems = await GetSelectListOfSprintInfoList(projectId)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddATask(
            AddATaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _taskService.Process(model.ToRequest());

                return RedirectToRoute(
                    nameof(GetTaskInfoList),
                    new { projectId = model.ProjectId });
            }
            return View(model);
        }

        public async Task<IActionResult> EditTheTask(Guid id)
        {
            var taskInfo = await _taskService.Process(new GetTheTaskInfo(id));
            var model = EditTheTaskViewModel.ToViewModel(taskInfo!);
            model.TaskStatusSelectListItems = await GetSelectListOfTaskStatus();
            model.SprintsInfoItems = await GetSelectListOfSprintInfoList(taskInfo!.ProjectId);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTheTask(
            EditTheTaskViewModel model, Guid projectId)
        {
            if (ModelState.IsValid)
            {
                await _taskService.Process(model.ToRequest());

                return RedirectToRoute(
                    nameof(GetTaskInfoList),
                    new { projectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ArchiveTheTask(Guid id)
        {
            var taskInfo = await _taskService.Process(
                new GetTheTaskInfo(id));
            var model = new ArchiveTheTaskViewModel
            {
                TaskInfo = taskInfo
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchiveConfirmed(ArchiveTheTaskViewModel model)
        {
            await _taskService.Process(new ArchiveTheTask(model.TaskInfo!.Id));

            return RedirectToRoute(
                 nameof(GetTaskInfoList),
                 new { model.TaskInfo!.ProjectId });
        }

        public async System.Threading.Tasks.Task ChangeTheTaskStatus(Guid id, Domain.TaskAggregation.TaskStatus status)
        {
            await _taskService.Process(new ChangeTheTaskStatus(id, status));
        }

        private async Task<List<SelectListItem>> GetSelectListOfSprintInfoList(Guid projectId)
        {
           return (await _sprintService.Process(
                new GetSprintInfoList().SetProjectId(projectId))).Items.ToSelectList(
                    value: item => item.Id!.ToString(),
                    text: item => item.Name);
        }
        private async Task<List<SelectListItem>> GetSelectListOfTaskStatus()
        {
            return
                (await _taskService.Process(new GetTaskStatusList()))
                .ToSelectList(
                    value: item => item.Key,
                    text: item => item.Value);
        }
    }
}
