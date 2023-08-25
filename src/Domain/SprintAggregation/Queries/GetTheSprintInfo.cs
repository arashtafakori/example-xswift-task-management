using CoreX.Domain;
using MediatR;

namespace Domain.SprintAggregation
{
    public class GetTheSprintInfo :
        ReadonlyRetrivalEntityRequestById<Sprint, Guid>,
        IRequest<SprintInfo?>
    {
        public GetTheSprintInfo(Guid id) : base(id)
        {
            ThrowExceptionIfEntityWasNotFound = true;
        }

        /// <summary>
        /// "To grant the permission for deleted items.".
        /// </summary>
        /// <returns></returns>
        public bool OnIncludingArchivedDataConfiguration()
        {
            return true;
        }
    }
}
