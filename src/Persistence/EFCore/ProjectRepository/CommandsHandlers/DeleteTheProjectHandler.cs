using MediatR;
using Module.Domain.ProjectAggregation;
using XSwift.Datastore;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.ProjectRepository
{
    public class DeleteTheProjectHandler :
        IRequestHandler<DeleteTheProject>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public DeleteTheProjectHandler(
           IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;

            _database.ResolveSoftDeleteConfiguration(
                new ModuleDeletabilityConfigurationFactory()
                .CreateInstance(database));
        }

        public async Task<Unit> Handle(
            DeleteTheProject request,
            CancellationToken cancellationToken)
        {
            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            await _database.DeleteAsync(request, entity);
            return new Unit();
        }
    }
}
