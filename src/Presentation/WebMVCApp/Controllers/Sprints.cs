using XSwift.Domain;
using XSwift.Mvc;
using Module.Domain.SprintAggregation;
using Microsoft.AspNetCore.Mvc;
using Module.Presentation.WebMVCApp.ViewModels;

namespace Module.Presentation.WebMVCApp.Controllers
{
    public class Sprints : XMvcController
    {
        private readonly HttpService _projectHttpService;
        private readonly HttpService _sprintHttpService;

        public Sprints(IHttpClientFactory httpClientFactory)
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
        }

        [Route($"{nameof(Projects)}/{{{nameof(Domain.SprintAggregation.GetSprintInfoList.ProjectId)}}}/[controller]", Name = nameof(GetSprintInfoList))]
        public async Task<IActionResult> GetSprintInfoList(
            GetSprintInfoListViewModel model,
            Guid projectId,
            int? pageNumber = null,
            int? pageSize = null)
        {
            model ??= new GetSprintInfoListViewModel();

            model.SprintInfoList = await _sprintHttpService
                .SendAndReadAsResultAsync<PaginatedViewModel<SprintInfo>>(
                new XHttpRequest(HttpMethod.Get, version: "v1.1",
                collectionResource: CollectionNames.Projects,
                collectionItemParameter: projectId,
                subCollectionResource: CollectionNames.Sprints,
                queryParametersString: HttpContext.Request.QueryString.ToString()));

            model.ProjectInfo = await DataFacilitator.GetProjectInfo(_projectHttpService, projectId);

            return View(model);
        }

        public async Task<IActionResult> GetInfo(Guid sprintId)
        {
            var sprintInfo = await DataFacilitator.GetSprintInfo(_sprintHttpService, sprintId);

            var model = new GetTheSprintInfoViewModel
            {
                SprintInfo = sprintInfo,
                ProjectInfo = await DataFacilitator.GetProjectInfo(_projectHttpService, sprintInfo.ProjectId)
            };
            
            return View(model);
        }

        public async Task<IActionResult> Define(Guid projectId)
        {
            var model = new DefineASprintViewModel()
            {
                ProjectId = projectId,
                ProjectInfo = await DataFacilitator.GetProjectInfo(_projectHttpService, projectId)
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DefiningConfirmed(
            DefineASprintViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _sprintHttpService.SendAsync(
                    new XHttpRequest(HttpMethod.Post, model.ToRequest()));

                return RedirectToRoute(
                    nameof(GetSprintInfoList),
                    new { projectId = model.ProjectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeTheSprintName(Guid sprintId)
        {
            var model = ChangeTheSprintNameViewModel.ToViewModel(
                await DataFacilitator.GetSprintInfo(_sprintHttpService, sprintId));
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangingTheSprintNameConfirmed(
            ChangeTheSprintNameViewModel model, Guid projectId)
        {
            if (ModelState.IsValid)
            {
                await _sprintHttpService.SendAsync(
                    new XHttpRequest(HttpMethod.Patch,
                    model.ToRequest(),
                    actionName: "ChangeTheProjectName"));

                return RedirectToRoute(
                    nameof(GetSprintInfoList),
                    new { projectId });
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeTheSprintTimeSpan(Guid sprintId)
        {
            var model = ChangeTheSprintTimeSpanViewModel.ToViewModel(
                await DataFacilitator.GetSprintInfo(_sprintHttpService, sprintId));
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangingTheSprintTimeSpanConfirmed(
            ChangeTheSprintTimeSpanViewModel model, Guid projectId)
        {
            if (ModelState.IsValid)
            {
                await _sprintHttpService.SendAsync(
                    new XHttpRequest(HttpMethod.Patch,
                    model.ToRequest(),
                    actionName: "ChangeTheSprintTimeSpan"));

                return RedirectToRoute(
                    nameof(GetSprintInfoList),
                    new { projectId });
            }
            return View(model);
        }

        public async Task<IActionResult> Archive(Guid sprintId)
        {
            var checkTheItemForArchiving = _sprintHttpService.SendAsync(
                    new XHttpRequest(HttpMethod.Get,
                    actionName: HttpServiceBasicActionsName.CheckTheItemForArchiving,
                    collectionItemParameter: sprintId));

            var model = new ArchiveTheSprintViewModel
            {
                SprintInfo = await DataFacilitator.GetSprintInfo(_sprintHttpService, sprintId),
                IssuesOfArchivingPossibility = (await CatchDomainErrors(
                    () => checkTheItemForArchiving))?.Issues
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArchivingConfirmed(ArchiveTheSprintViewModel model)
        {
            await _sprintHttpService.SendAsync(
                new XHttpRequest(HttpMethod.Patch,
                actionName: HttpServiceBasicActionsName.Archive,
                collectionItemParameter: model.SprintInfo!.Id,
                 queryParameters: new QueryParameters()
                 .AddParameter("archivingAllTasksMode", model.ArchivingAllTaskMode)));

             return RedirectToRoute(
                nameof(GetSprintInfoList),
                new { model.SprintInfo!.ProjectId });
        }
    }
}
