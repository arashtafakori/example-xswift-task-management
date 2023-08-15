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
        public Task<ProjectDetailsViewModel> Process(GetTheProjectDetails request);
        public Task<List<ProjectDetailsViewModel>> Process(GetSomeProjectDetails request);
    }
}
