using Domain.ProjectAggregation;

namespace MVCWebApp.Model.Projects
{
    public class ChangeTheProjectNameModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }

    public static partial class ViewModelExtensions
    {
        public static ChangeTheProjectNameModel 
            ToChangeTheProjectNameModel(this ProjectDetailsViewModel viewModel)
        {
            return new ChangeTheProjectNameModel()
            {
                Id = viewModel.Id,
                Name = viewModel.Name
            };
        }

        public static ChangeTheProjectName 
            ToRequest(this ChangeTheProjectNameModel model)
        {
            return new ChangeTheProjectName(model.Id, model.Name);
        }
    }
}
