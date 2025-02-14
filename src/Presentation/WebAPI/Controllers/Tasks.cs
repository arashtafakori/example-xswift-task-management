using Microsoft.AspNetCore.Mvc;
using Module.Contract;
using Module.Domain.TaskAggregation;
using XSwift.Mvc;
using XSwift.Domain;

namespace Module.Presentation.WebAPI
{
    [ApiController]
    [Route("v1/[controller]")]
    public class Tasks : XApiController
    {
        private readonly ITaskService _service;

        public Tasks(ITaskService service)
        {
            _service = service;
        }

        [HttpGet($"/v1/{nameof(Projects)}/{{{nameof(GetTaskInfoList.ProjectId)}}}/[controller]")]
        public async Task<ActionResult<PaginatedViewModel<TaskInfo>>> GetInfoList(
            Guid projectId,
            Guid? sprintId = null,
            Domain.TaskAggregation.TaskStatus? status = null,
            string? descriptionSearchKey = null)
        {
            var request = GetRequest<GetTaskInfoList>();
            request.SetProjectId(projectId);
            request.Setup(
                paginationSetting: new PaginationSetting(
                    defaultPageNumber: 1, defaultPageSize: 10));

            return await _service.Process(request);
        }
        [HttpGet($"/v1.1/{nameof(Projects)}/{{{nameof(GetTaskInfoList.ProjectId)}}}/[controller]")]
        public async Task<ActionResult<PaginatedViewModel<TaskInfo>>> GetInfoList(
            Guid projectId,
            Guid? sprintId = null,
            Domain.TaskAggregation.TaskStatus? status = null,
            string? descriptionSearchKey = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            var request = GetRequest<GetTaskInfoList>();
            request.SetProjectId(projectId);
            request.Setup(
                paginationSetting: new PaginationSetting(
                    defaultPageNumber: 1, defaultPageSize: 10));

            return await _service.Process(request);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskInfo?>> GetInfo(Guid id)
        {
            return await View(
                () => _service.Process(new GetTheTaskInfo(id)));
        }

        [HttpPost]
        public async Task<ActionResult<TaskInfo?>> Add(AddATask request)
        {
            var id = await _service.Process(request);
            return CreatedAtAction(nameof(GetInfo), new { id }, id);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditTheTask request)
        {
            return await View(
                () => _service.Process(request));
        }
        [HttpPatch("[action]")]
        public async Task<IActionResult> ChangeTheTaskStatus(ChangeTheTaskStatus request)
        {
            return await View(
                () => _service.Process(request));
        }
        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> Archive(Guid id)
        {
            return await View(
                () => _service.Process(new ArchiveTheTask(id)));
        }
        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> Restore(Guid id)
        {
            return await View(
                () => _service.Process(new RestoreTheTask(id)));
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<KeyValuePair<int, string>>>> GetTaskStatusItems()
        {
            var request = GetRequest<GetTaskStatusList>();
            return await _service.Process(request);
        }
    }
}