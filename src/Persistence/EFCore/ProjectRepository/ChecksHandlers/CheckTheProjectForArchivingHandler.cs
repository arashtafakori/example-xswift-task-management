using MediatR;
using Domain.ProjectAggregation;
using XSwift.Datastore;
using EntityFrameworkCore.XSwift.Datastore;

namespace Persistence.EFCore.ProjectRepository
{
    public class CheckTheProjectForArchivingHandler :
        IRequestHandler<CheckTheProjectForArchiving>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public CheckTheProjectForArchivingHandler(
           IMediator mediator,
           IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }

        public async Task<Unit> Handle(
            CheckTheProjectForArchiving request,
            CancellationToken cancellationToken)
        {
            await _database.CheckInvariantsAsync<
                 CheckTheProjectForArchiving, Project>(request);
            await request.ResolveAsync(_mediator);
            return new Unit();
        }
    }
}
