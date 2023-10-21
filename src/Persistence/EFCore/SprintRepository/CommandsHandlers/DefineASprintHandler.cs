using MediatR;
using Domain.SprintAggregation;
using EntityFrameworkCore.XSwift.Datastore;
using XSwift.Datastore;
using Domain.ProjectAggregation;

namespace Persistence.EFCore.SprintRepository
{
    public class DefineASprintHandler :
        IRequestHandler<DefineASprint, Guid>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public DefineASprintHandler(
            IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }
        public async Task<Guid> Handle(
            DefineASprint request,
            CancellationToken cancellationToken)
        {
            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            await _database.CreateAsync<DefineASprint, SprintEntity, Guid>(request, entity);
            return entity.Id;
        }
    }
} 
