using CoreX.Domain;
using Domain.TaskAggregation;

namespace Contract
{
    public interface ITaskService
    {
        public Task<Guid> Process(AddATask request);
        public System.Threading.Tasks.Task Process(EditTheTask request);
        public System.Threading.Tasks.Task Process(ArchiveTheTask request);
        public System.Threading.Tasks.Task Process(ChangeTheTaskStatus request);
        public System.Threading.Tasks.Task Process(RestoreTheTask request);
        public Task<TaskInfo?> Process(GetTheTaskInfo request);
        public Task<PaginatedViewModel<TaskInfo>> Process(GetTaskInfoList request);
        public Task<List<KeyValuePair<int, string>>> Process(GetTaskStatusList request);
    }
}
