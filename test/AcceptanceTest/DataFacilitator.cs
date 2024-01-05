using Module.Contract;
using Module.Domain.ProjectAggregation;
using Module.Domain.SprintAggregation;
using Module.Domain.TaskAggregation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AcceptanceTest
{
    public static class DataFacilitator
    {
        public static async Task<Guid> DefineAProject(
            IServiceScope _serviceScope, string name)
        {
            return await _serviceScope.ServiceProvider.
                GetRequiredService<IProjectService>().Process(
                new DefineAProject(name));
        }

        public static async Task<Guid> DefineASprint(
            IServiceScope _serviceScope, Guid projectId, string name)
        {
            return await _serviceScope.ServiceProvider.
                GetRequiredService<ISprintService>().Process(
                new DefineASprint(projectId, name));
        }

        public static async Task<Guid> AddATask(
            IServiceScope _serviceScope, Guid projectId, string description, Guid? sprintId = null)
        {
            return await _serviceScope.ServiceProvider.
                GetRequiredService<ITaskService>().Process(
                new AddATask(
                    projectId,
                    description: description,
                    sprintId));
        }
    }
}
