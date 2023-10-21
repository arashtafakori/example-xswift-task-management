﻿using Contract;
using XSwift.Domain;
using Domain.ProjectAggregation;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebMVCApp.ViewModels;
using XSwift.Mvc;

namespace Presentation.WebMVCApp.Controllers
{
    public class Projects : MvcControllerX
    {
        private readonly IProjectService _projectService;
        public Projects(
            IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult> GetProjectInfoList(
            GetProjectInfoListViewModel model,
            int? pageNumber = null,
            int? pageSize = null)
        {
            var request = GetRequest<GetProjectInfoList>();
            request.Setup(
                paginationSetting: new PaginationSetting(
                    defaultPageNumber: 1, defaultPageSize: 10));

            model ??= new GetProjectInfoListViewModel();
            model.ProjectInfoList = await _projectService.Process(request);

            return View(model);
        }

        public async Task<IActionResult> GetTheProjectInfo(Guid id)
        {
            var model = new GetTheProjectInfoViewModel
            {
                ProjectInfo = await _projectService.Process(
                new GetTheProjectInfo(id))
            };
            return View(model);
        }

        public IActionResult DefineAProject()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DefineAProject(
            DefineAProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _projectService.Process(model.ToRequest());
                return RedirectToAction(nameof(GetProjectInfoList));
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeTheProjectName(Guid id)
        {
            return View(ChangeTheProjectNameViewModel.ToViewModel(
                (await _projectService.Process(new GetTheProjectInfo(id)))!));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeTheProjectName(
            ChangeTheProjectNameViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _projectService.Process(model.ToRequest());
                return RedirectToAction(nameof(GetProjectInfoList));
            }
            return View(model);
        }

        public async Task<IActionResult> ArchiveTheProject(Guid id)
        {
            var model = new ArchiveTheProjectViewModel
            {
                ProjectInfo = await _projectService.Process(
                new GetTheProjectInfo(id)),
                IssuesOfArchivingPossibility = (await CatchDomainErrors(
                    () => _projectService.Process(new CheckTheProjectForArchiving(id))))
                    ?.Issues
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchivingTheProjectConfirmed(ArchiveTheProjectViewModel model)
        {
            await _projectService.Process(new ArchiveTheProject(model.ProjectInfo!.Id));
            return RedirectToAction(nameof(GetProjectInfoList));
        }

        public async Task<IActionResult> DeleteTheProject(Guid id)
        {
            var model = new DeleteTheProjectViewModel
            {
                ProjectInfo = await _projectService.Process(
                    new GetTheProjectInfo(id))
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletingTheProjectConfirmed(DeleteTheProjectViewModel model)
        {
            await _projectService.Process(new DeleteTheProject(model.ProjectInfo!.Id));
            return RedirectToAction(nameof(GetProjectInfoList));
        }
    }
}
