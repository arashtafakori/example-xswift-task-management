using MediatR;
using Domain.SprintAggregation;
using EntityFrameworkCore.CoreX.Datastore;
using CoreX.Datastore;

namespace Persistence.EFCore.SprintRepository
{
    public class ArchiveTheSprintHandler :
        IRequestHandler<ArchiveTheSprint>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public ArchiveTheSprintHandler(
           IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;

            _database.AddSoftDeleteConfiguration(
                new ModuleDeletabilityConfigurationFactory()
                .CreateInstance(database));
        }

        public async Task<Unit> Handle(
            ArchiveTheSprint request,
            CancellationToken cancellationToken)
        {
            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            await _database.ArchiveAsync(entity);
            return new Unit();
        }
    }
}
