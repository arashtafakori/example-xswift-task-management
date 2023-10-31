using Domain.ProjectAggregation;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebMVCApp.ViewModels;
using XSwift.Mvc;
using Microsoft.AspNetCore.Authorization;
using XSwift.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;

namespace Presentation.WebMVCApp.Controllers
{
    [Authorize]
    public class Projects : MvcControllerX
    {
        private HttpService ProjectApiService { get; set; }
        public Projects(IHttpClientFactory httpClientFactory)
        {
            var httpClient = httpClientFactory.CreateClient(HttpClientNames.WebAPIClient);

            ProjectApiService = new HttpService(
                httpClient: httpClient,
                version: "v1", 
                collectionResource: "Projects");
        }

        public async Task<IActionResult> GetProjectInfoList(
            GetProjectInfoListViewModel model,
            int? pageNumber = null,
            int? pageSize = null)
        {
            model ??= new GetProjectInfoListViewModel();
            model.ProjectInfoList = 
                await ProjectApiService.SendAsync<PaginatedViewModel<ProjectInfo>>(
                    HttpMethod.Get,
                    version: "v1.1",
                    queryParametersString: HttpContext.Request.QueryString.ToString());

            await LogTokenAndClaims();

            return View(model);
        }

        public async Task LogTokenAndClaims()
        {
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            Debug.WriteLine($"Identity token: {identityToken}");

            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }
        public async Task<IActionResult> GetTheProjectInfo(Guid projectId)
        {
            var model = new GetTheProjectInfoViewModel
            {
                ProjectInfo = await ApiServiceFacilitator.GetTheProjectInfo(
                    ProjectApiService, projectId)
            };
            return View(model);
        }

        //[Authorize(Roles = Configuration.AuthDefinitions.Roles.Admin)]
        public IActionResult DefineAProject()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DefiningAProjectConfirmed(
            DefineAProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                await ProjectApiService.SendAsync(HttpMethod.Post, model.ToRequest());

                return RedirectToAction(nameof(GetProjectInfoList));
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeTheProjectName(Guid projectId)
        {
            return View(ChangeTheProjectNameViewModel.ToViewModel(
                await ApiServiceFacilitator.GetTheProjectInfo(
                    ProjectApiService, projectId)));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangingTheProjectNameConfirmed(
            ChangeTheProjectNameViewModel model)
        {
            if (ModelState.IsValid)
            {
                await ProjectApiService.SendAsync(
                    HttpMethod.Patch,
                    model.ToRequest(),
                    actionName: "ChangeTheProjectName");

                return RedirectToAction(nameof(GetProjectInfoList));
            }
            return View(model);
        }

        public async Task<IActionResult> ArchiveTheProject(Guid projectId)
        {
            var model = new ArchiveTheProjectViewModel
            {
                ProjectInfo = await ApiServiceFacilitator.GetTheProjectInfo(
                    ProjectApiService, projectId),
                IssuesOfArchivingPossibility = (await CatchDomainErrors(
                    () => ProjectApiService.SendAsync(
                        HttpMethod.Get,
                        actionName: "CheckTheProjectForArchiving",
                        collectionItemParameter: projectId)))
                    ?.Issues
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchivingTheProjectConfirmed(ArchiveTheProjectViewModel model)
        {
            await ProjectApiService.SendAsync(
                HttpMethod.Patch,
                 actionName: "ArchiveTheProject",
                 collectionItemParameter: model.ProjectInfo!.Id);
 
            return RedirectToAction(nameof(GetProjectInfoList));
        }

        public async Task<IActionResult> DeleteTheProject(Guid projectId)
        {
            var model = new DeleteTheProjectViewModel
            {
                ProjectInfo = await ApiServiceFacilitator.GetTheProjectInfo(
                    ProjectApiService, projectId)
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletingTheProjectConfirmed(DeleteTheProjectViewModel model)
        {
            await ProjectApiService.SendAsync(
                HttpMethod.Delete,
                collectionItemParameter: model.ProjectInfo!.Id);
            return RedirectToAction(nameof(GetProjectInfoList));
        }
    }
}
