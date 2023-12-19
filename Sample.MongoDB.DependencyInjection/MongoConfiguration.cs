namespace Sample.MongoDB.DependencyInjection
{
    public class MongoConfiguration
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";

        public string DatabaseName { get; set; } = "mongo_api";

        public bool LogStatements { get; set; } = false;
    }
}
