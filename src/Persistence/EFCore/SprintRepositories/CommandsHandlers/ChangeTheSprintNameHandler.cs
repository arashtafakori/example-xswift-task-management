﻿using CoreX.Datastore;
using Domain.SprintAggregation;
using EntityFrameworkCore.CoreX.Datastore;
using MediatR;

namespace Persistence.EFCore.SprintRepository
{
    public class ChangeTheSprintNameHandler :
        IRequestHandler<ChangeTheSprintName>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public ChangeTheSprintNameHandler(
            IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }

        public async Task<Unit> Handle(
            ChangeTheSprintName request,
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
