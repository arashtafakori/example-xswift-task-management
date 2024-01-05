using MediatR;
using Module.Domain.SprintAggregation;
using XSwift.Datastore;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.SprintRepository
{
    public class RestoreTheSprintHandler :
        IRequestHandler<RestoreTheSprint>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public RestoreTheSprintHandler(
           IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;

            _database.ResolveSoftDeleteConfiguration(
                new ModuleDeletabilityConfigurationFactory()
                .CreateInstance(database));
        }

        public async Task<Unit> Handle(
            RestoreTheSprint request,
            CancellationToken cancellationToken)
        {
            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            await _database.RestoreAsync(request , entity);
            return new Unit();
        }
    }
}
