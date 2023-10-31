using XSwift.Datastore;
using Domain.ProjectAggregation;
using MediatR;
using EntityFrameworkCore.XSwift;

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
            await _database.UpdateAsync(request , entity);
            return new Unit();
        }
    }
}
