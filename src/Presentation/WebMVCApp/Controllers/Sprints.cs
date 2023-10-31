using XSwift.Domain;
using XSwift.Mvc;
using Domain.SprintAggregation;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebMVCApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.WebMVCApp.Controllers
{
    [Authorize]
    public class Sprints : MvcControllerX
    {
        private HttpService ProjectApiService { get; set; }
        private HttpService SprintApiService { get; set; }

        public Sprints(IHttpClientFactory httpClientFactory)
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
        }

        [Route($"{nameof(Projects)}/{{{nameof(Domain.SprintAggregation.GetSprintInfoList.ProjectId)}}}/[controller]", Name = nameof(GetSprintInfoList))]
        public async Task<IActionResult> GetSprintInfoList(
            GetSprintInfoListViewModel model,
            Guid projectId,
            int? pageNumber = null,
            int? pageSize = null)
        {
            model ??= new GetSprintInfoListViewModel();

            model.SprintInfoList =
                await SprintApiService.SendAsync<PaginatedViewModel<SprintInfo>>(
                    HttpMethod.Get,
                    version: "v1.1",
                    collectionResource: "Projects",
                    collectionItemParameter: projectId,
                    subCollectionResource: "Sprints",
                    queryParametersString: HttpContext.Request.QueryString.ToString());
            model.ProjectInfo = await ApiServiceFacilitator.GetTheProjectInfo(
                    ProjectApiService, projectId);

            return View(model);
        }

        public async Task<IActionResult> GetTheSprintInfo(Guid sprintId)
        {
            var sprintInfo = await ApiServiceFacilitator.GetTheSprintInfo(
                    SprintApiService, sprintId);

            var model = new GetTheSprintInfoViewModel
            {
                SprintInfo = sprintInfo,
                ProjectInfo = await ApiServiceFacilitator.GetTheProjectInfo(
                    ProjectApiService, sprintInfo.ProjectId)
            };

            return View(model);
        }

        public async Task<IActionResult> DefineASprint(Guid projectId)
        {
            var model = new DefineASprintViewModel()
            {
                ProjectId = projectId,
                ProjectInfo = await ApiServiceFacilitator.GetTheProjectInfo(
                    ProjectApiService, projectId)
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DefiningASprintConfirmed(
            DefineASprintViewModel model)
        {
            if (ModelState.IsValid)
            {
                await SprintApiService.SendAsync(HttpMethod.Post, model.ToRequest());

                return RedirectToRoute(
                    nameof(GetSprintInfoList),
                    new { projectId = model.ProjectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeTheSprintName(Guid sprintId)
        {
            var model = ChangeTheSprintNameViewModel.ToViewModel(
                await ApiServiceFacilitator.GetTheSprintInfo(
                    SprintApiService, sprintId));
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangingTheSprintNameConfirmed(
            ChangeTheSprintNameViewModel model, Guid projectId)
        {
            if (ModelState.IsValid)
            {
                await SprintApiService.SendAsync(
                    HttpMethod.Patch,
                    model.ToRequest(),
                    actionName: "ChangeTheSprintName");

                return RedirectToRoute(
                    nameof(GetSprintInfoList),
                    new { projectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeTheSprintTimeSpan(Guid sprintId)
        {
            var model = ChangeTheSprintTimeSpanViewModel.ToViewModel(
                await ApiServiceFacilitator.GetTheSprintInfo(
                    SprintApiService, sprintId));
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangingTheSprintTimeSpanConfirmed(
            ChangeTheSprintTimeSpanViewModel model, Guid projectId)
        {
            if (ModelState.IsValid)
            {
                await SprintApiService.SendAsync(
                    HttpMethod.Patch,
                    model.ToRequest(),
                    actionName: "ChangeTheSprintTimeSpan");

                return RedirectToRoute(
                    nameof(GetSprintInfoList),
                    new { projectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ArchiveTheSprint(Guid sprintId)
        {
            var model = new ArchiveTheSprintViewModel
            {
                SprintInfo = await ApiServiceFacilitator.GetTheSprintInfo(
                    SprintApiService, sprintId),
                IssuesOfArchivingPossibility = (await CatchDomainErrors(
                    () => SprintApiService.SendAsync(
                        HttpMethod.Get,
                        actionName: "CheckTheSprintForArchiving",
                        collectionItemParameter: sprintId)))
                    ?.Issues
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchivingTheSprintConfirmed(ArchiveTheSprintViewModel model)
        {
            var dd = HttpContext.Request.QueryString.ToString();

            await SprintApiService.SendAsync(
                HttpMethod.Patch,
                 actionName: "ArchiveTheSprint",
                 collectionItemParameter: model.SprintInfo!.Id,
                 queryParameters: new QueryParameters()
                 .AddParameter("archivingAllTasksMode", model.ArchivingAllTaskMode));

            return RedirectToRoute(
                nameof(GetSprintInfoList),
                new { model.SprintInfo!.ProjectId });
        }
    }
}
