using XSwift.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    public class GetProjectInfoList :
        QueryListRequest<Project>, 
        IRequest<PaginatedViewModel<ProjectInfo>>
    {
        public GetProjectInfoList()
        {
            ValidationState.Validate();
        }
    }
}
