using Microsoft.AspNetCore.Mvc;
using Contract;
using Domain.SprintAggregation;
using CoreX.Web;

namespace Presentation.WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class SprintsController : CoreXApiController
    {
        private readonly ISprintService _service;
            
        public SprintsController(ISprintService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SprintInfo?>> Get(Guid id)
        {
            return await ResloveIfAnEntityNotFound(
                () => _service.Process(new GetTheSprintInfo(id)));
        }

        [HttpPost($"{nameof(DefineANewSprint)}")]
        public async Task<ActionResult<Guid>> Post(DefineANewSprint request)
        {
            return CreatedAtAction(nameof(Get), await _service.Process(request));
        }

        [HttpPatch($"{nameof(ChangeTheSprintName)}")]
        public async Task<IActionResult> Patch(ChangeTheSprintName request)
        {
            return await ResloveIfAnEntityNotFound(
                () => _service.Process(request));
        }
        [HttpPatch($"{nameof(ChangeTheSprintTimeSpan)}")]
        public async Task<IActionResult> Patch(ChangeTheSprintTimeSpan request)
        {
            return await ResloveIfAnEntityNotFound(
                () => _service.Process(request));
        }
        [HttpPatch($"{nameof(ArchiveTheSprint)}/{{id}}")]
        public async Task<IActionResult> ArchiveTheSprint(Guid id)
        {
            return await ResloveIfAnEntityNotFound(
                () => _service.Process(new ArchiveTheSprint(id)));
        }

        [HttpPatch($"{nameof(RestoreTheSprint)}/{{id}}")]
        public async Task<IActionResult> RestoreTheSprint(Guid id)
        {
            return await ResloveIfAnEntityNotFound(
                () => _service.Process(new RestoreTheSprint(id)));
        }
    }
}