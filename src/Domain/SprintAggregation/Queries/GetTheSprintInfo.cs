using XSwift.Domain;
using MediatR;

namespace Domain.SprintAggregation
{
    public class GetTheSprintInfo :
        QueryItemRequestById<Sprint, Guid>,
        IRequest<SprintInfo?>
    {
        public GetTheSprintInfo(Guid id) : base(id)
        {
            PreventIfNoEntityWasFound = true;
        }
    }
}
