using Application;
using Contract;
using CoreX.Web;
using Domain.ProjectAggregation;
using Domain.SprintAggregation;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Model;
using static MassTransit.ValidationResultExtensions;

namespace MVCWebApp.Controllers
{
    public class ProjectsController : CoreXMVCController
    {
        private readonly IProjectService _projectService;
        public ProjectsController(
            IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult> GetSomeProjectInfo()
        {
            var request = RestApiHelper<GetSomeProjectInfo>
                           .QueryStringToObject(
                HttpContext.Request.QueryString.ToString());
            var result = await _projectService.Process(request);

            return View(result);
        }

        public async Task<IActionResult> GetTheProjectInfo(Guid id)
        {
            var result = await _projectService.Process(
                new GetTheProjectInfo(id));
            return View(result);
        }

        public IActionResult DefineANewProject()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DefineANewProject(
            ViewModelAsDefineANewProject model)
        {
            if (ModelState.IsValid)
            {
                await ResloveIfAnEntityNotFound(
                   async () => {
                       await _projectService.Process(model.ConvertToARequest());
                   });
                return RedirectToAction(nameof(GetSomeProjectInfo));
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeTheProjectName(Guid id)
        {
            var result = await _projectService.Process(new GetTheProjectInfo(id));
            return View(ViewModelAsChangeTheProjectName.GetViewModel(result!));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeTheProjectName(
            ViewModelAsChangeTheProjectName model)
        {
            if (ModelState.IsValid)
            {
                await ResloveIfAnEntityNotFound(
                   () => _projectService.Process(model.ConvertToARequest()));
                return RedirectToAction(nameof(GetSomeProjectInfo));
            }
            return View(model);
        }

        public async Task<IActionResult> ArchiveTheProject(Guid id)
        {
            var result = await _projectService.Process(
                new GetTheProjectInfo(id));
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchiveConfirmed(Guid id)
        {
            await ResloveIfAnEntityNotFound(
                () => _projectService.Process(new ArchiveTheProject(id)));

            return RedirectToAction(nameof(GetSomeProjectInfo));
        }
    }
}
