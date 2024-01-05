using MediatR;
using Module.Domain.TaskAggregation;
using XSwift.Datastore;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.TaskRepository
{
    public class AddATaskHandler :
        IRequestHandler<AddATask, Guid>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public AddATaskHandler(
            IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }
        public async Task<Guid> Handle(
            AddATask request,
            CancellationToken cancellationToken)
        {
            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            await _database.CreateAsync<AddATask, TaskEntity, Guid>(request, entity);
            return entity.Id;
        }
    }
} 
