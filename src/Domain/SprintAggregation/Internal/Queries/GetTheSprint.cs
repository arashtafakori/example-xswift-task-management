using XSwift.Domain;
using MediatR;

namespace Domain.SprintAggregation
{
    internal class GetTheSprint :
        QueryItemRequestById<Sprint, Guid>,
        IRequest<Sprint?>
    {

        public GetTheSprint(Guid id,
            bool evenArchivedData = false)
            : base(id)
        {
            TrackingMode = true;
            PreventIfNoEntityWasFound = true;
            EvenArchivedData = evenArchivedData;
        }
    }
}
