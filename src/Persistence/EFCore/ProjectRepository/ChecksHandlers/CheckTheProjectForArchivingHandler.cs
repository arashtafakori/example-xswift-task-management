using MediatR;
using Domain.ProjectAggregation;
using XSwift.Datastore;
using EntityFrameworkCore.XSwift.Datastore;

namespace Persistence.EFCore.ProjectRepository
{
    public class CheckTheProjectForArchivingHandler :
        IRequestHandler<CheckTheProjectForArchiving, bool>
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

        public async Task<bool> Handle(
            CheckTheProjectForArchiving request,
            CancellationToken cancellationToken)
        {
            var result = await _database.AnyAsync<
                 CheckTheProjectForArchiving, ProjectEntity>(request);
            await request.ResolveAsync(_mediator);
            return result;
        }
    }
}
