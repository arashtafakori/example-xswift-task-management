using Domain.ProjectAggregation;

namespace Contract
{
    public interface IProjectService
    {
        public Task<Guid> Process(DefineANewProject request);
        public Task Process(ChangeTheProjectName request);
        public Task Process(ArchiveTheProject request);
        public Task Process(RestoreTheProject request);
        public Task Process(DeleteTheProject request);
        public Task<ProjectInfo?> Process(GetTheProjectInfo request);
        public Task<List<ProjectInfo>> Process(GetSomeProjectInfo request);
    }
}
