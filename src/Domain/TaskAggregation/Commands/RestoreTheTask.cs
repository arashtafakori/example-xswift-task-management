﻿using XSwift.Domain;
using MediatR;

namespace Domain.TaskAggregation
{
    public class RestoreTheTask :
        RequestToRestoreById<Task, Guid>, IRequest
    {
        public RestoreTheTask(Guid id) : base(id)
        {
            ValidationState.Validate();
        }
        public override async Task<Task> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var task = (await mediator.Send(
                new GetTheTask(Id, evenArchivedData: true)))!;
            await base.ResolveAsync(mediator, task);
            return task;
        }
    }
}
