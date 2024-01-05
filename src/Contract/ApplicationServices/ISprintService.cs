using XSwift.Domain;
using Module.Domain.SprintAggregation;

namespace Module.Contract
{
    public interface ISprintService
    {
        public Task<Guid> Process(DefineASprint request);
        public Task Process(ChangeTheSprintName request);
        public Task Process(ChangeTheSprintTimeSpan request);
        public Task Process(ArchiveTheSprint request);
        public Task Process(CheckTheSprintForArchiving request);
        public Task Process(RestoreTheSprint request);
        public Task<SprintInfo?> Process(GetTheSprintInfo request);
        public Task<PaginatedViewModel<SprintInfo>> Process(GetSprintInfoList request);
    }
}
