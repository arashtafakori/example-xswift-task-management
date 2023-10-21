using Domain.ProjectAggregation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Persistence.MongoDb
{
    public class ProjectConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Project>(cm =>
            {
                cm.AutoMap(); 
                cm.MapMember(p => p.Id).SetSerializer(new GuidSerializer(BsonType.ObjectId));
            });
        }
    }
}
