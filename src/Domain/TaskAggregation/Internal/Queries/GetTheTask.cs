using CoreX.Domain;
using MediatR;

namespace Domain.TaskAggregation
{
    internal class GetTheTask :
        QueryItemRequestById<Task, Guid>,
        IRequest<Task?>
    {
        public GetTheTask(Guid id,
            bool evenArchivedData = false)
            : base(id)
        {
            TrackingMode = true;
            PreventIfNoEntityWasFound = true;
            EvenArchivedData = evenArchivedData;
        }
    }
}
