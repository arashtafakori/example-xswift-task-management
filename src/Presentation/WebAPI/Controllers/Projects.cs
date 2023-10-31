using Microsoft.AspNetCore.Mvc;
using Contract;
using Domain.ProjectAggregation;
using XSwift.Mvc;
using XSwift.Domain;
using Microsoft.AspNetCore.Authorization;
using Presentation.Configuration.AuthDefinitions;

namespace Presentation.WebAPI
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize(Policies.ClientId)]
    [Authorize(Policies.ProjectsSettingsScope)]
    public class Projects : ApiControllerX
    {
        private readonly IProjectService _projectService;
        private readonly ISprintService _sprintService;
        private readonly ITaskService _taskService;
        public Projects(
            IProjectService projectService,
            ISprintService sprintService,
            ITaskService taskService)
        {
            _projectService = projectService;
            _sprintService = sprintService;
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedViewModel<ProjectInfo>>> Get()
        {
            var request = GetRequest<GetProjectInfoList>();
            return await _projectService.Process(request);
        }

        /// <summary>
        /// You can send a querystring like the following example,
        /// for adjusting the offset and limit values
        /// Example: offset=0&limit=5
        /// </summary>
        /// <returns></returns>
        [HttpGet($"/v1.1/[controller]")]
        public async Task<ActionResult<PaginatedViewModel<ProjectInfo>>> GetProjectInfoList(
            int? pageNumber = null,
            int? pageSize = null)
        {
            var request = GetRequest<GetProjectInfoList>();
            request.Setup(
                paginationSetting: new PaginationSetting(
                    defaultPageNumber: 1, defaultPageSize: 10));

            return await _projectService.Process(request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectInfo?>> GetTheProjectInfo(Guid id)
        {
            return await View(
                () => _projectService.Process(new GetTheProjectInfo(id)));
        }

        [HttpPost]
        public async Task<ActionResult<ProjectInfo?>> DefineAProject(DefineAProject request)
        {
            var id = await _projectService.Process(request);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpPatch("[action]")]
        public async Task<IActionResult> ChangeTheProjectName(ChangeTheProjectName request)
        {
            return await View(
                () => _projectService.Process(request));
        }

        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> ArchiveTheProject(Guid id)
        {
            return await View(
                () => _projectService.Process(new ArchiveTheProject(id)));
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> CheckTheProjectForArchiving(Guid id)
        {
            return await View(
                () => _projectService.Process(new CheckTheProjectForArchiving(id)));
        }
        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> RestoreTheProject(Guid id)
        {
            return await View(
                () => _projectService.Process(new RestoreTheProject(id)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheProject(Guid id)
        {
            return await View(
                () => _projectService.Process(new DeleteTheProject(id)));
        }
    }
}