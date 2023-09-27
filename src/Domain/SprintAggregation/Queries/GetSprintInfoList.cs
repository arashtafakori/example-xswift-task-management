using CoreX.Domain;
using MediatR;
using System.Linq.Expressions;

namespace Domain.SprintAggregation
{
    public class GetSprintInfoList :
        QueryListRequest<Sprint>, 
        IRequest<PaginatedViewModel<SprintInfo>>
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
        public override Expression<Func<Sprint, bool>>? Condition()
        {
            return x => x.ProjectId == ProjectId;
        }
    }
}
