using MediatR;
using Domain.SprintAggregation;
using EntityFrameworkCore.CoreX.Datastore;
using CoreX.Datastore;

namespace Persistence.EFCore.SprintRepository
{
    public class DefineANewSprintHandler :
        IRequestHandler<DefineANewSprint, Guid>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public DefineANewSprintHandler(
            IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }
        public async Task<Guid> Handle(
            DefineANewSprint request,
            CancellationToken cancellationToken)
        {
            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            await _database.CreateAsync(
                entity,
                uniqueSpecification: entity.UniqueSpecification());
            return entity.Id;
        }
    }
} 
