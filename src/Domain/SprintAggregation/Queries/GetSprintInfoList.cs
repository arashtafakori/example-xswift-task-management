using XSwift.Domain;
using MediatR;
using XSwift.Base;

namespace Module.Domain.SprintAggregation
{
    public class GetSprintInfoList :
        QueryListRequest<SprintEntity, PaginatedViewModel<SprintInfo>>
    {
        public Guid ProjectId { get; private set; }
        public GetSprintInfoList()
        {
            ValidationState.Validate();
        }

        public GetSprintInfoList SetProjectId(Guid value)
        {
            ProjectId = value;
            return this;
        }

        public override ExpressionBuilder<SprintEntity> Where()
        {
            WhereExpression.And(x => x.ProjectId == ProjectId);
            return base.Where();
        }

        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
