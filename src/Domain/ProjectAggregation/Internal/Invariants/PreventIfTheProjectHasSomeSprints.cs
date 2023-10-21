using MediatR;
using XSwift.Base;
using XSwift.Domain;

namespace Domain.ProjectAggregation
{
    internal class PreventIfTheProjectHasSomeSprints
        : InvariantRequestById<ProjectEntity, Guid>
    {
        public PreventIfTheProjectHasSomeSprints(Guid id) : base(id)
        {
        }

        public override IIssue? GetIssue()
        {
            return new TheProjectHasSomeSprints();
        }
        public override async Task ResolveAsync(IMediator mediator)
        {
            await InvariantState.AssestAsync(mediator);
        }
    }
}
