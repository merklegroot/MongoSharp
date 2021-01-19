using MongoDB.Driver;

namespace mongo_lib
{
    public interface IMongoDatabaseContext
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        IMongoClient GetClient();
        IMongoDatabase GetDatabase();
    }
}
