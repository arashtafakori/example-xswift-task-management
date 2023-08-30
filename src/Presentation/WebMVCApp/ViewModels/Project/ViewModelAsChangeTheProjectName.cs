using Domain.ProjectAggregation;

namespace MVCWebApp.Model
{
    public class ViewModelAsChangeTheProjectName
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public static ViewModelAsChangeTheProjectName GetViewModel(ProjectInfo viewModel)
        {
            return new ViewModelAsChangeTheProjectName()
            {
                Id = viewModel.Id,
                Name = viewModel.Name
            };
        }

        public ChangeTheProjectName ConvertToARequest()
        {
            return new ChangeTheProjectName(Id, Name);
        }
    }
}
