using MongoDB.Driver;

namespace mongo_lib
{
    public class MongoDatabaseContext : IMongoDatabaseContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        
        public MongoDatabaseContext() { }

        public MongoDatabaseContext(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

        public IMongoClient GetClient()
        {
            return new MongoClient(ConnectionString);
        }

        public IMongoDatabase GetDatabase()
        {
            return GetClient().GetDatabase(DatabaseName);
        }
    }
}
