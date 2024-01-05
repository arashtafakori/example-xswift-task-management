using Microsoft.AspNetCore.Mvc;
using XSwift.Mvc;
using Microsoft.AspNetCore.Authorization;
using Module.Presentation.Configuration.AuthDefinitions;
using XSwift.Datastore;
using XSwift.Base;

namespace Module.Presentation.WebAPI
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize(Policies.ClientsConstraint)]
    [Authorize(Policies.ToAccessToTheDevelopmentFeatures)]
    public class Development : XApiController
    {
        private readonly IDatabase _database;
        public Development(IDatabase database)
        {
            _database = database;
        }

        [HttpPost("[action]")]

        public void EnsureRecreatedDatabase()
        {
            XService.Environment.EnsureStateIsDevelopment();

            _database.EnsureRecreated();
        }
    }
}