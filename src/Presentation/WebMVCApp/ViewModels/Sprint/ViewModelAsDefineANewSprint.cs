using Domain.SprintAggregation;

namespace MVCWebApp.Model
{
    public class ViewModelAsDefineANewSprint
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }

        public DefineANewSprint ConvertToARequest()
        {
            return new DefineANewSprint(ProjectId, Name);
        }
    }
}
