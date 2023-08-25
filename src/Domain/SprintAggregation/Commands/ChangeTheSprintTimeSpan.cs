﻿using CoreX.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.SprintAggregation
{
    public class ChangeTheSprintTimeSpan :
        UpdationRequestById<ChangeTheSprintName, Sprint, Guid>, IRequest
    {
        [BindTo(typeof(Sprint), nameof(Sprint.StartDate))]
        [Required]
        public DateTime StartDate { get; private set; }

        [BindTo(typeof(Sprint), nameof(Sprint.EndDate))]
        [Required]
        public DateTime EndDate { get; private set; }

        public ChangeTheSprintTimeSpan(
            Guid id, DateTime startDate, DateTime endDate) : base(id)
        {
            StartDate = startDate;
            EndDate = endDate;
            ValidationState.Validate();
        }

        public override async Task<Sprint> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var invariantState = new InvariantState();

            var lastYear = DateTime.Now.AddYears(-1);
            if (StartDate < lastYear || EndDate < lastYear)
                invariantState.AddIssue(
                    new TheStartDateAndEndDateOfTheSprintCanNotBeLaterThanTheLastTwelveMonths());

            if (StartDate > EndDate)
                invariantState.AddIssue(
                    new TheStartDateOfTheSprintCanNotBeLaterThanTheEndDate());

            await invariantState.CheckAsync(mediator);

            //

            var entity = await mediator.Send(new RetriveTheSprint(Id));
            entity!.SetStartDate(StartDate).SetEndDate(EndDate);
            await base.ResolveAsync(mediator, entity!);
            return entity!;
        }
    }
}
