﻿using MediatR;
using XSwift.Datastore;
using Domain.TaskAggregation;
using EntityFrameworkCore.XSwift.Datastore;

namespace Persistence.EFCore.TaskRepository
{
    internal class GetTheTaskHandler :
        IRequestHandler<GetTheTask, TaskEntity?>
    {
        private readonly Database _database;
        public GetTheTaskHandler(IDatabase database)
        {
            _database = (Database)database;
        }

        public async Task<TaskEntity?> Handle(
            GetTheTask request,
            CancellationToken cancellationToken)
        {
            return await _database.GetItemAsync<GetTheTask,TaskEntity>(request: request);
        }
    }
}
