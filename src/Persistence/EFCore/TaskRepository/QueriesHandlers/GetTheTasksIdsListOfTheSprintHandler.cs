﻿using MediatR;
using XSwift.Datastore;
using Module.Domain.TaskAggregation;
using XSwift.EntityFrameworkCore;

namespace Module.Persistence.TaskRepository
{
    internal class GetTheTasksIdsListOfTheSprintHandler :
        IRequestHandler<GetTheTasksIdsListOfTheSprint, List<Guid>>
    {
        private readonly IMediator _mediator;
        private readonly Database _database;
        public GetTheTasksIdsListOfTheSprintHandler(
            IMediator mediator, IDatabase database)
        {
            _mediator = mediator;
            _database = (Database)database;
        }

        public async Task<List<Guid>> Handle(
            GetTheTasksIdsListOfTheSprint request,
            CancellationToken cancellationToken)
        {
            var tasksIds = await _database.GetListAsync(
                           request: request,
                           selector: (IQueryable<TaskEntity> query) => {
                               return from task in query
                                      select task.Id; 
                           });

            return tasksIds;
        }
    }
}
