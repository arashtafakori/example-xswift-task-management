using Contract;
using Domain.TaskAggregation;
using Domain.ProjectAggregation;
using Domain.SprintAggregation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AcceptanceTest
{
    public class ApplicationServiceFacilitator
    {
        private readonly IServiceScope _serviceScope;
        public ApplicationServiceFacilitator(IServiceScope serviceScope)
        {
            _serviceScope = serviceScope;
        }
        public async Task<Guid> DefineAProject(string projectName)
        {
            return await _serviceScope.ServiceProvider.
                GetRequiredService<IProjectService>().Process(
                new DefineAProject(projectName));
        }
        public async Task<Guid> DefineASprint(Guid projectId, string sprintName)
        {
            return await _serviceScope.ServiceProvider.
                GetRequiredService<ISprintService>().Process(
                new DefineASprint(projectId, sprintName));
        }
        public async Task<Guid> AddATask(
            Guid projectId,
            string description,
            Guid? sprintId)
        {
            return await _serviceScope.ServiceProvider.
                GetRequiredService<ITaskService>().Process(
                new AddATask(projectId, description,sprintId));
        }
    }
}
