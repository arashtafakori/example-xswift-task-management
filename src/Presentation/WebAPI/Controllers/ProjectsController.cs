using Microsoft.AspNetCore.Mvc;
using Contract;
using Domain.ProjectAggregation;
using CoreX.Web;
using Domain.SprintAggregation;

namespace Presentation.WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : CoreXApiController
    {
        private readonly IProjectService _projectService;
        private readonly ISprintService _sprintService;

        public ProjectsController(
            IProjectService projectService, ISprintService sprintService)
        {
            _projectService = projectService;
            _sprintService = sprintService;
        }

        /// <summary>
        /// You can send a querystring like the following example,
        /// for adjusting the offset and limit values
        /// Example: offset=0&limit=5
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectInfo>>>Get()
        {
            var request = RestApiHelper<GetSomeProjectInfo>
                           .QueryStringToObject(
                HttpContext.Request.QueryString.ToString());
            return await _projectService.Process(request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectInfo?>> Get(Guid id)
        {
            return await ResloveIfAnEntityNotFound(
                () => _projectService.Process(new GetTheProjectInfo(id)));
        }

        [HttpGet("{id}/sprints")]
        public async Task<ActionResult<IEnumerable<SprintInfo>>> GetSprints(Guid id)
        {
            var request = RestApiHelper<GetSomeSprintInfo>
                           .QueryStringToObject(
                HttpContext.Request.QueryString.ToString())
                           .SetProjectId(id);
            return await _sprintService.Process(request);
        }

        [HttpPost($"{nameof(DefineANewProject)}")]
        public async Task<ActionResult<Guid>> Post(DefineANewProject request)
        {
            return CreatedAtAction(nameof(Get), await _projectService.Process(request));
        }

        [HttpPatch($"{nameof(ChangeTheProjectName)}")]
        public async Task<IActionResult> Patch(ChangeTheProjectName request)
        {
            return await ResloveIfAnEntityNotFound(
                () => _projectService.Process(request));
        }

        [HttpPatch($"{nameof(ArchiveTheProject)}/{{id}}")]
        public async Task<IActionResult> ArchiveTheProject(Guid id)
        {
            return await ResloveIfAnEntityNotFound(
                () => _projectService.Process(new ArchiveTheProject(id)));
        }

        [HttpPatch($"{nameof(RestoreTheProject)}/{{id}}")]
        public async Task<IActionResult> RestoreTheProject(Guid id)
        {
            return await ResloveIfAnEntityNotFound(
                () => _projectService.Process(new RestoreTheProject(id)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await ResloveIfAnEntityNotFound(
                () => _projectService.Process(new DeleteTheProject(id)));
        }
    }
}