using MediatR;
using XSwift.Domain;

namespace Module.Domain.ProjectAggregation
{
    public class GetProjectInfoList :
        QueryListRequest<ProjectEntity, PaginatedViewModel<ProjectInfo>>
    {
        public GetProjectInfoList()
        {
            ValidationState.Validate();
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
