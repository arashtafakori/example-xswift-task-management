using CoreX.Datastore;
using Domain.ProjectAggregation;
using EntityFrameworkCore.CoreX.Datastore;
using MediatR;

namespace Persistence.EFCore.ProjectRepository
{
    public class ChangeTheProjectNameHandler :
        IRequestHandler<ChangeTheProjectName>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public ChangeTheProjectNameHandler(
            IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }

        public async Task<Unit> Handle(
            ChangeTheProjectName request,
            CancellationToken cancellationToken)
        {
            var entity = await request.ResolveAndGetEntityAsync(_mediator);
            await _database.UpdateAsync(
                condition: request.Condition()!,
                uniqueSpecification: entity.UniqueSpecification());
            return new Unit();
        }
    }
}
