using Domain.SprintAggregation;

namespace Contract
{
    public interface ISprintService
    {
        public Task<Guid> Process(DefineANewSprint request);
        public Task Process(ChangeTheSprintName request);
        public Task Process(ChangeTheSprintTimeSpan request);
        public Task Process(ArchiveTheSprint request);
        public Task Process(RestoreTheSprint request);
        public Task<SprintInfo?> Process(GetTheSprintInfo request);
        public Task<List<SprintInfo>> Process(GetSomeSprintInfo request);
    }
}
