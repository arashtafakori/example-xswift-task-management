using CoreX.Domain;
using MediatR;

namespace Domain.SprintAggregation
{
    public class GetSomeSprintInfo :
        ReadonlyRetrivalEntitiesRequest<Sprint>, 
        IRequest<List<SprintInfo>>
    {
        public int Limit { get; internal set; }
        public int Offset { get; internal set; }

        public GetSomeSprintInfo(
            int limit = 20,
            int offset = 0)
        {
            Limit = limit;
            Offset = offset;

            ValidationState.Validate();
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
