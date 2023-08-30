using CoreX.Domain;
using MediatR;
using System.Linq.Expressions;

namespace Domain.SprintAggregation
{
    public class GetSomeSprintInfo :
        ReadonlyRetrivalEntitiesRequest<Sprint>, 
        IRequest<List<SprintInfo>>
    {
        public Guid ProjectId { get; set; }
        public GetSomeSprintInfo(
            int offset, int limit) :
            base(offset: offset, limit: limit)
        {
            ValidationState.Validate();
        }

        public GetSomeSprintInfo SetProjectId(Guid value)
        {
            ProjectId = value;
            return this;
        }

        /// <summary>
        /// "To grant the permission for deleted items.".
        /// </summary>
        /// <returns></returns>
        public bool OnIncludingArchivedDataConfiguration()
        {
            return true;
        }

        public override Expression<Func<Sprint, bool>>? Condition()
        {
            return x => x.ProjectId == ProjectId;
        }
    }
}
