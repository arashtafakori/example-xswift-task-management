using MediatR;
using Domain.ProjectAggregation;
using EntityFrameworkCore.XSwift.Datastore;
using XSwift.Datastore;

namespace Persistence.EFCore.ProjectRepository
{
    public class RestoreTheProjectHandler :
        IRequestHandler<RestoreTheProject>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public RestoreTheProjectHandler(
           IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;

            _database.ResolveSoftDeleteConfiguration(
                new ModuleDeletabilityConfigurationFactory()
                .CreateInstance(database));
        }

        public async Task<Unit> Handle(
            RestoreTheProject request,
            CancellationToken cancellationToken)
        {
            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            await _database.RestoreAsync(request, entity);
            return new Unit();
        }
    }
}
