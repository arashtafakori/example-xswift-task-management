using MediatR;
using Domain.ProjectAggregation;
using EntityFrameworkCore.XSwift.Datastore;
using XSwift.Datastore;

namespace Persistence.EFCore.ProjectRepository
{
    public class DefineAProjectHandler :
        IRequestHandler<DefineAProject, Guid>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public DefineAProjectHandler(
            IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }
        public async Task<Guid> Handle(
            DefineAProject request,
            CancellationToken cancellationToken)
        {
            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            await _database.CreateAsync(request, entity);
            return entity.Id;
        }
    }
} 
