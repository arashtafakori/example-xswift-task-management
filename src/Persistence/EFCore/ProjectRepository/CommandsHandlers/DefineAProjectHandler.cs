using MediatR;
using Domain.ProjectAggregation;
using XSwift.Datastore;
using EntityFrameworkCore.XSwift;

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
            await _database.CreateAsync<DefineAProject, ProjectEntity, Guid>(request, entity);
            return entity.Id;
        }
    }
} 
