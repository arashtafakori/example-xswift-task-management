using Contract;
using Domain.ProjectAggregation;
using Domain.SprintAggregation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AcceptanceTest
{
    public class DataFacilitator
    {
        private readonly IServiceScope _serviceScope;
        public DataFacilitator(IServiceScope serviceScope)
        {
            _serviceScope = serviceScope;
        }
        public async Task<Guid> DefineAProject(string projectName)
        {
            return await _serviceScope.ServiceProvider.
                GetRequiredService<IProjectService>().Process(
                new DefineANewProject(projectName));
        }
        public async Task<Guid> DefineASprint(Guid projectId, string sprintName)
        {
            return await _serviceScope.ServiceProvider.
                GetRequiredService<ISprintService>().Process(
                new DefineANewSprint(projectId, sprintName));
        }
    }
}
