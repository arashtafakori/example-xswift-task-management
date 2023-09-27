using Contract;
using CoreX.Domain;
using CoreX.Mvc;
using Domain.ProjectAggregation;
using Domain.SprintAggregation;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebMVCApp.ViewModels;

namespace Presentation.WebMVCApp.Controllers
{
    public class Sprints : XMvcController
    {
        private readonly IProjectService _projectService;
        private readonly ISprintService _sprintService;

        public Sprints(
            IProjectService projectService,
            ISprintService sprintService)
        {
            _projectService = projectService;
            _sprintService = sprintService;
        }

        [Route($"{nameof(Projects)}/{{{nameof(Domain.SprintAggregation.GetSprintInfoList.ProjectId)}}}/[controller]", Name = nameof(GetSprintInfoList))]
        public async Task<IActionResult> GetSprintInfoList(
            GetSprintInfoListViewModel model,
            Guid projectId,
            int? pageNumber = null,
            int? pageSize = null)
        {
            var request = GetRequest<GetSprintInfoList>()
                           .SetProjectId(projectId);
            request.Setup(
                paginationSetting: new PaginationSetting(
                    defaultPageNumber: 1, defaultPageSize: 10));

            model ??= new GetSprintInfoListViewModel();
            model.SprintInfoList = await _sprintService.Process(request);
            model.ProjectInfo = await _projectService.Process(new GetTheProjectInfo(projectId));

            return View(model);
        }

        public async Task<IActionResult> GetTheSprintInfo(Guid id)
        {
            var sprintInfo = await _sprintService.Process(
                new GetTheSprintInfo(id));
            var model = new GetTheSprintInfoViewModel
            {
                SprintInfo = sprintInfo,
                ProjectInfo = await _projectService.Process(new GetTheProjectInfo(sprintInfo!.ProjectId))
            };

            return View(model);
        }

        public async Task<IActionResult> DefineASprint(Guid projectId)
        {
            var model = new DefineASprintViewModel()
            {
                ProjectId = projectId,
                ProjectInfo = await _projectService.Process(new GetTheProjectInfo(projectId))
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DefineASprint(
            DefineASprintViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _sprintService.Process(model.ToRequest());
                return RedirectToRoute(
                    nameof(GetSprintInfoList),
                    new { projectId = model.ProjectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeTheSprintName(Guid id)
        {
            var sprintInfo = await _sprintService.Process(new GetTheSprintInfo(id));
            var model = ChangeTheSprintNameViewModel.ToViewModel(sprintInfo!);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeTheSprintName(
            ChangeTheSprintNameViewModel model, Guid projectId)
        {
            if (ModelState.IsValid)
            {
                await _sprintService.Process(model.ToRequest());
                return RedirectToRoute(
                    nameof(GetSprintInfoList),
                    new { projectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeTheSprintTimeSpan(Guid id)
        {
            var sprintInfo = await _sprintService.Process(new GetTheSprintInfo(id));
            var model = ChangeTheSprintTimeSpanViewModel.ToViewModel(sprintInfo!);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeTheSprintTimeSpan(
            ChangeTheSprintTimeSpanViewModel model, Guid projectId)
        {
            if (ModelState.IsValid)
            {
                await _sprintService.Process(model.ToRequest());
                return RedirectToRoute(
                    nameof(GetSprintInfoList),
                    new { projectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ArchiveTheSprint(Guid id)
        {
            var sprintInfo = await _sprintService.Process(
                new GetTheSprintInfo(id));
            var model = new ArchiveTheSprintViewModel
            {
                SprintInfo = sprintInfo!,
                Issues = (await CatchDomainErrors(
                    () => _sprintService.Process(new CheckTheSprintForArchiving(id))))
                    ?.Issues
            };



            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchiveConfirmed(ArchiveTheSprintViewModel model)
        {
            await _sprintService.Process(model.ToRequest());
            return RedirectToRoute(
                nameof(GetSprintInfoList),
                new { model.SprintInfo!.ProjectId });
        }
    }
}
