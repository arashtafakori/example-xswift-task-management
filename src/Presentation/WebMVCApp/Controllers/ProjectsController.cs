using Contract;
using CoreX.Web;
using Domain.ProjectAggregation;
using MassTransit.Internals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebApp.Model.Projects;

namespace MVCWebApp.Controllers.Project
{
    public class ProjectsController : CoreXMVCController
    {
        private readonly IProjectService _service;

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        public async Task<IActionResult> List()
        {
            var request = RestApiHelper<GetSomeProjectDetails>
                           .QueryStringToObject(
                HttpContext.Request.QueryString.ToString());
            var viewModels = await _service.Process(request);

            return View(viewModels);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var viewModel = await _service.Process(
                new GetTheProjectDetails(id));
            return View(viewModel);
        }

        public async Task<IActionResult> ChangeTheProjectName(Guid id)
        {
            var viewModel = await _service.Process(new GetTheProjectDetails(id));
            return View(viewModel.ToChangeTheProjectNameModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeTheProjectName(
            [Bind("Id,Name")] ChangeTheProjectNameModel model)
        {
            if (ModelState.IsValid)
            {
                var request = model.ToRequest();
                await ResloveIfAnEntityNotFound(
                   () => _service.Process(request));
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        public IActionResult DefineANewProject()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DefineANewProject(
            [Bind("Name")] DefineANewProjectModel model)
        {
            if (ModelState.IsValid)
            {
                var request = model.ToRequest();
                await ResloveIfAnEntityNotFound(
                   async () => {
                       await _service.Process(request);
                   });
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        public async Task<IActionResult> ArchiveTheProject(Guid id)
        {
            var viewModel = await _service.Process(
                new GetTheProjectDetails(id));
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchiveConfirmed(Guid id)
        {
            await ResloveIfAnEntityNotFound(
                () => _service.Process(new ArchiveTheProject(id)));

            return RedirectToAction(nameof(List));
        }
    }
}
