using MediatR;
using Domain.ProjectAggregation;
using XSwift.Datastore;
using EntityFrameworkCore.XSwift;

namespace Persistence.EFCore.ProjectRepository
{
    internal class PreventIfTheProjectHasSomeSprintsHandler :
        IRequestHandler<PreventIfTheProjectHasSomeSprints, bool>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public PreventIfTheProjectHasSomeSprintsHandler(
           IMediator mediator,
           IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }

        public async Task<bool> Handle(
            PreventIfTheProjectHasSomeSprints request,
            CancellationToken cancellationToken)
        {
            var result = await _database.AnyAsync(
                request,
                filter: delegate (IQueryable<ProjectEntity> query)
                {
                    return from project in query
                           where
                                 project.Id == request.Id && project.Sprints.Any()
                           select project;
                });
            await request.ResolveAsync(_mediator);
            return result;
        }
    }
}
