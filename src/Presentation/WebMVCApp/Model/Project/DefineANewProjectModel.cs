using Domain.ProjectAggregation;

namespace MVCWebApp.Model.Projects
{
    public class DefineANewProjectModel
    {
        public required string Name { get; set; }
    }

    public static partial class ViewModelExtensions
    {
        public static DefineANewProject
            ToRequest(this DefineANewProjectModel model)
        {
            return new DefineANewProject(model.Name);
        }
    }
}
