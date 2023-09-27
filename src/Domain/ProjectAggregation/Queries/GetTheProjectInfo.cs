using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class GetTheProjectInfo :
        QueryItemRequestById<Project, Guid>,
        IRequest<ProjectInfo?>
    {
        public GetTheProjectInfo(Guid id) : base(id)
        {
            PreventIfNoEntityWasFound = true;
        }
    }
}
