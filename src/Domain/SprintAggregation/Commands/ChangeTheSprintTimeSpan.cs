using XSwift.Base;
using XSwift.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Domain.SprintAggregation
{
    public class ChangeTheSprintTimeSpan :
        RequestToUpdateById<SprintEntity, Guid>
    {
        [BindTo(typeof(SprintEntity), nameof(SprintEntity.StartDate))]
        [Required]
        public DateTime StartDate { get; private set; }

        [BindTo(typeof(SprintEntity), nameof(SprintEntity.EndDate))]
        [Required]
        public DateTime EndDate { get; private set; }

        public ChangeTheSprintTimeSpan(
            Guid id, DateTime startDate, DateTime endDate) : base(id)
        {
            StartDate = startDate;
            EndDate = endDate;

            ValidationState.AddAnValidation(
                new PreventIfStartDateIsLaterThanEndDate<SprintEntity>
                (StartDate, EndDate));

            ValidationState.Validate();
        }

        public override async Task<SprintEntity> ResolveAndGetEntityAsync(
            IMediator mediator)
        {
            var lastYear = DateTimeHelper.UtcNow.AddYears(-1);
            InvariantState.DefineAnInvariant(
                condition: () => { return StartDate < lastYear || EndDate < lastYear; },
                issue: new TheStartDateAndEndDateOfTheSprintCanNotBeEarlierThanTheLastTwelveMonths());

            await InvariantState.AssestAsync(mediator);

            var sprint = (await mediator.Send(new GetTheSprint(Id)))!;
            sprint.SetStartDate(StartDate).SetEndDate(EndDate);
            await base.ResolveAsync(mediator, sprint);
            return sprint;
        }
    }
}
