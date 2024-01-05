using Module.Domain.ProjectAggregation;

namespace Module.Presentation.WebMVCApp.ViewModels
{
    public class ChangeTheProjectNameViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public static ChangeTheProjectNameViewModel ToViewModel(ProjectInfo info)
        {
            return new ChangeTheProjectNameViewModel()
            {
                Id = info.Id,
                Name = info.Name
            };
        }

        public ChangeTheProjectName ToRequest()
        {
            return new ChangeTheProjectName(Id, Name);
        }
    }
}
