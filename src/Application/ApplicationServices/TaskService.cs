﻿using MediatR;
using XSwift.Datastore;
using Module.Contract;
using Module.Domain.TaskAggregation;
using XSwift.Domain;

namespace Module.Application
{
    public class TaskService : ITaskService
    {
        private readonly IMediator _mediator;
        private readonly IDbTransaction _transaction;

        public TaskService(
            IMediator mediator,
            IDbTransaction transaction)
        {
            _mediator = mediator;
            _transaction = transaction;
        }
        public async Task<Guid> Process(AddATask request)
        {
            var id = await _mediator.Send(request);
            await _transaction.SaveChangesAsync();
            return id;
        }
        public async Task Process(EditTheTask request)
        {
            await _mediator.Send(request);
            await _transaction.SaveChangesAsync(concurrencyCheck: true);
        }
        public async Task Process(ArchiveTheTask request)
        {
            await _mediator.Send(request);
            await _transaction.SaveChangesAsync(concurrencyCheck: true);
        }
        public async Task Process(ChangeTheTaskStatus request)
        {
            await _mediator.Send(request);
            await _transaction.SaveChangesAsync(concurrencyCheck: true);
        }
        public async Task Process(RestoreTheTask request)
        {
            await _mediator.Send(request);
            await _transaction.SaveChangesAsync(concurrencyCheck: true);
        }
        public async Task<TaskInfo?> Process(GetTheTaskInfo request)
        {
            return await _mediator.Send(request);
        }

        public async Task<PaginatedViewModel<TaskInfo>> Process(GetTaskInfoList request)
        {
            return await _mediator.Send(request);
        }
        public async Task<List<KeyValuePair<int, string>>> Process(GetTaskStatusList request)
        {
            return await _mediator.Send(request);
        }
    }
}
