using Microsoft.AspNetCore.Mvc;
using Contract;
using Domain.ProjectAggregation;
using CoreX.Web;

namespace Presentation.WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : CoreXApiController
    {
        private readonly IProjectService _service;
            
        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDetailsViewModel>>>Get()
        {
            var request = RestApiHelper<GetSomeProjectDetails>
                           .QueryStringToObject(
                HttpContext.Request.QueryString.ToString());
            var viewModels = await _service.Process(request);

            return viewModels;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDetailsViewModel>> Get(Guid id)
        {
            var viewModel = await ResloveIfAnEntityNotFound(
                () => _service.Process(new GetTheProjectDetails(id)));

            return viewModel;
        }

        [HttpPost($"{nameof(DefineANewProject)}")]
        public async Task<ActionResult<Guid>> Post(DefineANewProject request)
        {
            return CreatedAtAction(nameof(Get), await _service.Process(request));
        }

        [HttpPatch($"{nameof(ChangeTheProjectName)}")]
        public async Task<IActionResult> Patch(ChangeTheProjectName request)
        {
            return await ResloveIfAnEntityNotFound(
                () => _service.Process(request));
        }

        [HttpPatch($"{nameof(ArchiveTheProject)}/{{id}}")]
        public async Task<IActionResult> ArchiveTheProject(Guid id)
        {
            return await ResloveIfAnEntityNotFound(
                () => _service.Process(new ArchiveTheProject(id)));
        }

        [HttpPatch($"{nameof(RestoreTheProject)}/{{id}}")]
        public async Task<IActionResult> RestoreTheProject(Guid id)
        {
            return await ResloveIfAnEntityNotFound(
                () => _service.Process(new RestoreTheProject(id)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await ResloveIfAnEntityNotFound(
                () => _service.Process(new DeleteTheProject(id)));
        }
    }
}