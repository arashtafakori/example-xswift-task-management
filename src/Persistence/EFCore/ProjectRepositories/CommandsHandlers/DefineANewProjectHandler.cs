using MediatR;
using Domain.ProjectAggregation;
using EntityFrameworkCore.CoreX.Datastore;
using CoreX.Datastore;

namespace Persistence.EFCore.ProjectRepository
{
    public class DefineANewProjectHandler :
        IRequestHandler<DefineANewProject, Guid>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public DefineANewProjectHandler(
            IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }
        public async Task<Guid> Handle(
            DefineANewProject request,
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
