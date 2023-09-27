using MediatR;
using Domain.ProjectAggregation;
using EntityFrameworkCore.CoreX.Datastore;
using CoreX.Datastore;

namespace Persistence.EFCore.ProjectRepository
{
    public class ArchiveTheProjectHandler :
        IRequestHandler<ArchiveTheProject>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public ArchiveTheProjectHandler(
           IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;

            _database.ResolveSoftDeleteConfiguration(
                new ModuleDeletabilityConfigurationFactory()
                .CreateInstance(database));
        }

        public async Task<Unit> Handle(
            ArchiveTheProject request,
            CancellationToken cancellationToken)
        {
            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            await _database.ArchiveAsync(request, entity);
            return new Unit();
        }
    }
}
