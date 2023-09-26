using Domain.ProjectAggregation;

namespace Presentation.WebMVCApp.ViewModels
{
    public class ChangeTheProjectNameViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public static ChangeTheProjectNameViewModel ToViewModel(ProjectInfo projectInfo)
        {
            return new ChangeTheProjectNameViewModel()
            {
                Id = projectInfo.Id,
                Name = projectInfo.Name
            };
        }

        public ChangeTheProjectName ToRequest()
        {
            return new ChangeTheProjectName(Id, Name);
        }
    }
}
