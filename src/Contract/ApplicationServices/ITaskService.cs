using XSwift.Domain;
using Domain.TaskAggregation;

namespace Contract
{
    public interface ITaskService
    {
        public Task<Guid> Process(AddATask request);
        public Task Process(EditTheTask request);
        public Task Process(ArchiveTheTask request);
        public Task Process(ChangeTheTaskStatus request);
        public Task Process(RestoreTheTask request);
        public Task<TaskInfo?> Process(GetTheTaskInfo request);
        public Task<PaginatedViewModel<TaskInfo>> Process(GetTaskInfoList request);
        public Task<List<KeyValuePair<int, string>>> Process(GetTaskStatusList request);
    }
}
