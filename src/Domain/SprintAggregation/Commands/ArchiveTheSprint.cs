﻿using XSwift.Domain;
using Domain.TaskAggregation;
using MediatR;

namespace Domain.SprintAggregation
{
    public class ArchiveTheSprint :
        RequestToArchiveById<Sprint, Guid>, IRequest
    {
        public bool ArchivingAllTaskMode { get; private set; }
        public ArchiveTheSprint(
            Guid id,
            bool archivingAllTaskMode = false) : base(id)
        {
            ArchivingAllTaskMode = archivingAllTaskMode;
            ValidationState.Validate();
        }
        public override async Task<Sprint> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var sprint = (await mediator.Send(
                new GetTheSprint(Id, evenArchivedData: true)))!;
            await base.ResolveAsync(mediator, sprint);

            if(ArchivingAllTaskMode)
            {
                await mediator.Send(new SetTheTasksOfTheSprintToNoSprint()
                    .SetSprintId(Id));
            }
            else
            {
                var tasksIdsList = await mediator.Send(new GetTheTasksIdsListOfTheSprint()
                    .SetSprintId(Id));

                foreach(var taskId in tasksIdsList) 
                    await mediator.Send(new ChangeTheTaskSprint(id: taskId, sprintId: null));
            }

            return sprint;
        }
    }
}