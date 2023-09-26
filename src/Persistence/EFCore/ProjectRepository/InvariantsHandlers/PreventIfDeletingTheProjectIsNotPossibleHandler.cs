using MediatR;
using Domain.ProjectAggregation;
using CoreX.Datastore;
using EntityFrameworkCore.CoreX.Datastore;

namespace Persistence.EFCore.ProjectRepository
{
    internal class PreventIfDeletingTheProjectIsNotPossibleHandler :
        IRequestHandler<PreventIfDeletingTheProjectIsNotPossible>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public PreventIfDeletingTheProjectIsNotPossibleHandler(
           IMediator mediator,
           IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }

        public async Task<Unit> Handle(
            PreventIfDeletingTheProjectIsNotPossible request,
            CancellationToken cancellationToken)
        {
            await _database.CheckInvariantsAsync<
                 PreventIfDeletingTheProjectIsNotPossible, Project>(request);
            await request.ResolveAsync(_mediator);
            return new Unit();
        }
    }
}
