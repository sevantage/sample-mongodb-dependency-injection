using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sample.MongoDB.DependencyInjection
{
    [BsonIgnoreExtraElements]
    public class MyDocument
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Name { get; set; } = string.Empty;
    }
}
