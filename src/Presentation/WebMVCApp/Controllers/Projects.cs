using Module.Domain.ProjectAggregation;
using Microsoft.AspNetCore.Mvc;
using Module.Presentation.WebMVCApp.ViewModels;
using XSwift.Mvc;
using Microsoft.AspNetCore.Authorization;
using XSwift.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;

namespace Module.Presentation.WebMVCApp.Controllers
{
    public class Projects : XMvcController
    {
        private readonly HttpService _projectHttpService;
        public Projects(IHttpClientFactory httpClientFactory)
        {
            var httpClient = httpClientFactory.CreateClient(HttpClientNames.WebAPIClient);
          
            _projectHttpService = new HttpService(
                httpClient: httpClient,
                version: "v1", 
                collectionResource: CollectionNames.Projects);
        }

        public async Task<IActionResult> GetInfoList(
            int? pageNumber = null,
            int? pageSize = null)
        {
            var model = new GetProjectInfoListViewModel();

            model.ProjectInfoList = await _projectHttpService
                .SendAndReadAsResultAsync<PaginatedViewModel<ProjectInfo>>(
                new XHttpRequest(HttpMethod.Get, version: "v1.1",
                queryParametersString: HttpContext.Request.QueryString.ToString()));

            return View(model);
        }
        public async Task<IActionResult> GetInfo(Guid projectId)
        {
            var model = new GetTheProjectInfoViewModel
            {
                ProjectInfo = await DataFacilitator.GetProjectInfo(_projectHttpService, projectId)
            };
            return View(model);
        }

        public IActionResult Define()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DefiningConfirmed(
            DefineAProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _projectHttpService.SendAsync(
                    new XHttpRequest(HttpMethod.Post, model.ToRequest()));

                return RedirectToAction(nameof(GetInfoList));
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeTheProjectName(Guid projectId)
        {
            return View(ChangeTheProjectNameViewModel.ToViewModel(
                await DataFacilitator.GetProjectInfo(_projectHttpService, projectId)));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangingTheProjectNameConfirmed(
            ChangeTheProjectNameViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _projectHttpService.SendAsync(
                    new XHttpRequest(HttpMethod.Patch, 
                    model.ToRequest(),
                    actionName: "ChangeTheProjectName"));

                return RedirectToAction(nameof(GetInfoList));
            }
            return View(model);
        }

        public async Task<IActionResult> Archive(Guid projectId)
        {
            var checkTheItemForArchiving = _projectHttpService.SendAsync(
                    new XHttpRequest(HttpMethod.Get,
                    actionName: HttpServiceBasicActionsName.CheckTheItemForArchiving,
                    collectionItemParameter: projectId));

            var model = new ArchiveTheProjectViewModel
            {
                ProjectInfo = await DataFacilitator.GetProjectInfo(_projectHttpService, projectId),
                IssuesOfArchivingPossibility = (await CatchDomainErrors(
                    () => checkTheItemForArchiving))?.Issues
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchivingConfirmed(ArchiveTheProjectViewModel model)
        {
            await _projectHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                actionName: HttpServiceBasicActionsName.Archive,
                collectionItemParameter: model.ProjectInfo!.Id));

            return RedirectToAction(nameof(GetInfoList));
        }

        public async Task<IActionResult> Delete(Guid projectId)
        {
            var model = new DeleteTheProjectViewModel
            {
                ProjectInfo = await DataFacilitator.GetProjectInfo(_projectHttpService, projectId)
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletingConfirmed(DeleteTheProjectViewModel model)
        {
            await _projectHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Delete,
                collectionItemParameter: model.ProjectInfo!.Id));

            return RedirectToAction(nameof(GetInfoList));
        }
    }
}
