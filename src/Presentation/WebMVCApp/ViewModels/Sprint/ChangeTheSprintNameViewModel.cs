using Domain.SprintAggregation;

namespace Presentation.WebMVCApp.ViewModels
{
    public class ChangeTheSprintNameViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public SprintInfo? SprintInfo { get; set; }
        public static ChangeTheSprintNameViewModel ToViewModel(SprintInfo sprintInfo)
        {
            return new ChangeTheSprintNameViewModel()
            {
                Id = sprintInfo.Id,
                Name = sprintInfo.Name,
                SprintInfo = sprintInfo,
            };
        }

        public ChangeTheSprintName ToRequest()
        {
            return new ChangeTheSprintName(Id, Name);
        }
    }
}
