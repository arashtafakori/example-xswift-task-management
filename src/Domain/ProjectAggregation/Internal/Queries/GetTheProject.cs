using CoreX.Domain;
using MediatR;

namespace Domain.ProjectAggregation
{
    internal class GetTheProject :
        QueryItemRequestById<Project,  Guid>,
        IRequest<Project?>
    {
        public GetTheProject(Guid id,
            bool evenArchivedData = false)
            : base(id)
        {
            TrackingMode = true;
            PreventIfNoEntityWasFound = true;
            EvenArchivedData = evenArchivedData;
        }
    }
}
