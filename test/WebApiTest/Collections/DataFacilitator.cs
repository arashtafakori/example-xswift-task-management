using Module.Domain.ProjectAggregation;
using System.Threading.Tasks;
using System;
using XSwift.Mvc;
using System.Net.Http;
using Module.Domain.SprintAggregation;
using Module.Domain.TaskAggregation;

namespace WebApiTest
{
    public static class DataFacilitator
    {
        public static async Task<Guid> DefineAProject(
            HttpService _httpService, string name)
        {
            return await _httpService.SendAndReadAsResultAsync<Guid>(
                new XHttpRequest(HttpMethod.Post, new DefineAProject(name)));
        }

        public static async Task<Guid> DefineASprint(
            HttpService _httpService, Guid projectId, string name)
        {
            return await _httpService.SendAndReadAsResultAsync<Guid>(
                new XHttpRequest(HttpMethod.Post, new DefineASprint(projectId, name)));
        }

        public static async Task<Guid> AddATask(
            HttpService _httpService, Guid projectId, string description, Guid? sprintId = null)
        {
            return await _httpService.SendAndReadAsResultAsync<Guid>(
                new XHttpRequest(HttpMethod.Post, new AddATask(projectId, description, sprintId)));
        }
    }
}
