using Contract;
using CoreX.Domain;
using CoreX.Web;
using Domain.ProjectAggregation;
using Domain.SprintAggregation;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Model;
using static MassTransit.ValidationResultExtensions;

namespace MVCWebApp.Controllers
{
    public class SprintsController : CoreXMVCController
    {
        private readonly IProjectService _projectService;
        private readonly ISprintService _sprintService;

        public SprintsController(
            IProjectService projectService,
            ISprintService sprintService)
        {
            _projectService = projectService;
            _sprintService = sprintService;
        }

        [Route("Projects/{projectId}/Sprints", Name = nameof(GetSomeSprintInfo))]
        public async Task<IActionResult> GetSomeSprintInfo(Guid projectId)
        {
            var request = RestApiHelper<GetSomeSprintInfo>
                           .QueryStringToObject(
                HttpContext.Request.QueryString.ToString())
                           .SetProjectId(projectId);
            var result = await _sprintService.Process(request);

            ViewData[nameof(ProjectInfo)] =
                await _projectService.Process(new GetTheProjectInfo(projectId));

            return View(result);
        }

        public async Task<IActionResult> GetTheSprintInfo(Guid id)
        {
            var result = await _sprintService.Process(
                new GetTheSprintInfo(id));

            ViewData[nameof(ProjectInfo)] =
                await _projectService.Process(new GetTheProjectInfo(result!.ProjectId));

            return View(result);
        }

        public async Task<IActionResult> DefineANewSprint(Guid projectId)
        {
            var model = new ViewModelAsDefineANewSprint
            {
                ProjectId = projectId
            };

            ViewData[nameof(ProjectInfo)] =
                await _projectService.Process(new GetTheProjectInfo(projectId));

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DefineANewSprint(
            ViewModelAsDefineANewSprint model)
        {
            if (ModelState.IsValid)
            {
                await ResloveIfAnEntityNotFound(
                   async () => {
                       await _sprintService.Process(model.ConvertToARequest());
                   });

                return RedirectToRoute(
                    nameof(GetSomeSprintInfo),
                    new { projectId = model.ProjectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeTheSprintName(Guid id)
        {
            var result = await _sprintService.Process(new GetTheSprintInfo(id));

            ViewData[nameof(ProjectInfo)] =
                await _projectService.Process(new GetTheProjectInfo(result!.ProjectId));

            return View(ViewModelAsChangeTheSprintName.GetViewModel(result!));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeTheSprintName(
            ViewModelAsChangeTheSprintName model, Guid projectId)
        {
            if (ModelState.IsValid)
            {
                await ResloveIfAnEntityNotFound(
                   () => _sprintService.Process(model.ConvertToARequest()));

                return RedirectToRoute(
                    nameof(GetSomeSprintInfo),
                    new { projectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeTheSprintTimeSpan(Guid id)
        {
            var result = await _sprintService.Process(new GetTheSprintInfo(id));

            ViewData[nameof(SprintInfo)] = result;
            ViewData[nameof(ProjectInfo)] =
                await _projectService.Process(new GetTheProjectInfo(result!.ProjectId));

            return View(ViewModelAsChangeTheSprintTimeSpan.GetViewModel(result!));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeTheSprintTimeSpan(
            ViewModelAsChangeTheSprintTimeSpan model, Guid projectId)
        {
            if (ModelState.IsValid)
            {
                await ResloveIfAnEntityNotFound(
                   () => _sprintService.Process(model.ConvertToARequest()));

                return RedirectToRoute(
                    nameof(GetSomeSprintInfo),
                    new { projectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ArchiveTheSprint(Guid id)
        {
            var result = await _sprintService.Process(
                new GetTheSprintInfo(id));
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchiveConfirmed(Guid id, Guid projectId)
        {
            await ResloveIfAnEntityNotFound(
                () => _sprintService.Process(new ArchiveTheSprint(id)));

            return RedirectToRoute(
                nameof(GetSomeSprintInfo),
                new { projectId });
        }
    }
}
