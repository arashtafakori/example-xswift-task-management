using Microsoft.AspNetCore.Mvc;
using Contract;
using Domain.SprintAggregation;
using CoreX.Mvc;
using CoreX.Domain;

namespace Presentation.WebAPI
{
    [ApiController]
    [Route("v1/[controller]")]
    public class Sprints : XApiController
    {
        private readonly ISprintService _service;

        public Sprints(ISprintService service)
        {
            _service = service;
        }
        [HttpGet($"/v1/{nameof(Projects)}/{{{nameof(Domain.SprintAggregation.GetSprintInfoList.ProjectId)}}}/[controller]")]
        public async Task<ActionResult<PaginatedViewModel<SprintInfo>>> GetSprintInfoList(
            Guid projectId)
        {
            var request = GetRequest<GetSprintInfoList>()
                           .SetProjectId(projectId);
            return await _service.Process(request);
        }
        [HttpGet($"/v1.1/{nameof(Projects)}/{{{nameof(Domain.SprintAggregation.GetSprintInfoList.ProjectId)}}}/[controller]")]
        public async Task<ActionResult<PaginatedViewModel<SprintInfo>>> GetSprintInfoList(
            Guid projectId, 
            int? pageNumber = null,
            int? pageSize = null)
        {
            var request = GetRequest<GetSprintInfoList>()
                           .SetProjectId(projectId);
            request.Setup(
                paginationSetting: new PaginationSetting(
                    defaultPageNumber: 1, defaultPageSize: 10));

            return await _service.Process(request);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SprintInfo?>> GetTheSprintInfo(Guid id)
        {
            return await View(
                () => _service.Process(new GetTheSprintInfo(id)));
        }

        [HttpPost]
        public async Task<ActionResult<SprintInfo?>> DefineASprint(DefineASprint request)
        {
            var id = await _service.Process(request);
            return CreatedAtAction(nameof(GetTheSprintInfo), new { id }, id);
        }

        [HttpPatch("[action]")]
        public async Task<IActionResult> ChangeTheSprintName(ChangeTheSprintName request)
        {
            return await View(
                () => _service.Process(request));
        }
        [HttpPatch("[action]")]
        public async Task<IActionResult> ChangeTheSprintTimeSpan(ChangeTheSprintTimeSpan request)
        {
            return await View(
                () => _service.Process(request));
        }
        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> ArchiveTheSprint(
            Guid id, bool archivingAllTaskMode)
        {
            var request = GetRequest<ArchiveTheSprint>();
            request.SetId(id);

            return await View(
                () => _service.Process(request));
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> CheckTheSprintForArchiving(Guid id)
        {
            return await View(
                () => _service.Process(new CheckTheSprintForArchiving(id)));
        }
        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> RestoreTheSprint(Guid id)
        {
            return await View(
                () => _service.Process(new RestoreTheSprint(id)));
        }
    }
}