using XSwift.Domain;
using Domain.ProjectAggregation;

namespace Contract
{
    public interface IProjectService
    {
        public Task<Guid> Process(DefineAProject request);
        public Task Process(ChangeTheProjectName request);
        public Task Process(ArchiveTheProject request);
        public Task Process(CheckTheProjectForArchiving request);
        public Task Process(RestoreTheProject request);
        public Task Process(DeleteTheProject request);
        public Task<ProjectInfo?> Process(GetTheProjectInfo request);
        public Task<PaginatedViewModel<ProjectInfo>> Process(GetProjectInfoList request);
    }
}
